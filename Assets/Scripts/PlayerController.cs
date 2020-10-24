using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D theRB;
    public Transform gunArm;
    //private Camera theCam;

    public Animator Anim;

    /*public GameObject bulletToFire;
     public Transform firePoint;

     public float timeBetweemShots;
     private float shotCounter;*/

    public SpriteRenderer bodySR;

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvincibility = .5f;
    [HideInInspector]
    public float dashCounter;
    private float dashCoolCount;
    [HideInInspector]
    public bool canMove = true;

    public List<Gun> availableGuns = new List<Gun>();
    [HideInInspector]
    public int currentGun;


    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //theCam = Camera.main;
       
        activeMoveSpeed = moveSpeed;

        UIController.instance.currentGun.sprite = availableGuns[currentGun].gunUI;
        UIController.instance.gunText.text = availableGuns[currentGun].weaponName;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (canMove && !LevelManager.instance.isPaused)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
            //transform.position += new Vector3(moveInput.x * Time.deltaTime*moveSpeed,moveInput.y* Time.deltaTime*moveSpeed,0f);
            theRB.velocity = moveInput * activeMoveSpeed;

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);

            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;
            }

            //rotate Gun arm
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(0, 0, angle);


            /*if(Input.GetMouseButtonDown(0))
            {
                Instantiate(bulletToFire,firePoint.position,firePoint.rotation);
                shotCounter = timeBetweemShots;
                AudioManger.instance.PlaySFX(12);
            }
            if(Input.GetMouseButton(0))
            {
                shotCounter -=Time.deltaTime;
                if(shotCounter <= 0)
                {
                    Instantiate(bulletToFire,firePoint.position,firePoint.rotation);
                    AudioManger.instance.PlaySFX(12);
                    shotCounter = timeBetweemShots;
                }
            }*/

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (availableGuns.Count > 0)
                {
                    currentGun++;
                    if (currentGun >= availableGuns.Count)
                    {
                        currentGun = 0;
                    }

                    SwitchGun();
                }
                else
                {
                    Debug.LogError("Player has no gun!");
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCount <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    Anim.SetTrigger("dash");
                    PlayerHealthController.instance.MakeInvincible(dashInvincibility);
                    AudioManger.instance.PlaySFX(8);
                }
            }
            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCount = dashCooldown;
                }

            }

            if (dashCoolCount > 0)
            {
                dashCoolCount -= Time.deltaTime;
            }




            if (moveInput != Vector2.zero)
            {
                Anim.SetBool("isMoving", true);
            }
            else
            {
                Anim.SetBool("isMoving", false);
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
            Anim.SetBool("isMoving", false);
        }
        
    }

    public void SwitchGun()
    {
        foreach (Gun theGun in availableGuns)
        {
            theGun.gameObject.SetActive(false);
        }

        availableGuns[currentGun].gameObject.SetActive(true);
        UIController.instance.currentGun.sprite = availableGuns[currentGun].gunUI;
        UIController.instance.gunText.text = availableGuns[currentGun].weaponName;
    }

}
