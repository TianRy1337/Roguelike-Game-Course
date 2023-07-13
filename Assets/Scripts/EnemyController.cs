using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    [Header("追蹤玩家")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    [Header("遠離")]
    public bool shouldRunAway;
    public float runawayRange;
    [Header("遊蕩")]
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;
    [Header("巡邏")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;
    [Header("射擊")]
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float shouldShootRange;
    [Header("其他數據")]
    public SpriteRenderer theBody;
    public Animator Anim;
    public int health = 150;
    public GameObject[] deathsplatters;
    public GameObject hitEffect;

    public bool shouldDropItem;
    public GameObject[] itemToDrop;
    public float itemDropPercent;


    // Start is called before the first frame update
    void Start()
    {
        if (shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            moveDirection = Vector3.zero;

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                if (shouldWander)
                {
                    if (wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        //move the enemy
                        moveDirection = wanderDirection;
                        if (wanderCounter <= 0)
                        {
                            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
                        }
                    }
                    if (pauseCounter > 0)
                    {
                        pauseCounter -= Time.deltaTime;

                        if (pauseCounter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLength * .75f, wanderLength * 1.25f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                        }
                    }
                }

                if (shouldPatrol)
                {
                    moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;

                    if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .2f)
                    {
                        currentPatrolPoint++;

                        if (currentPatrolPoint >= patrolPoints.Length)
                        {
                            currentPatrolPoint = 0;
                        }
                    }
                }
            }
            if (shouldRunAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < runawayRange)
            {
                moveDirection = transform.position - PlayerController.instance.transform.position;
            }

            /*else
            {
                moveDirection = Vector3.zero;
            } */

            moveDirection.Normalize();

            theRB.velocity = moveDirection * moveSpeed;



            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shouldShootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManger.instance.PlaySFX(13);
                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }

        if (moveDirection != Vector3.zero)
        {
            Anim.SetBool("isMoving", true);
        }
        else
        {
            Anim.SetBool("isMoving", false);
        }

    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        AudioManger.instance.PlaySFX(2);
        Instantiate(hitEffect, transform.position, transform.rotation);
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioManger.instance.PlaySFX(1);
            int selectedSplatter = Random.Range(0, deathsplatters.Length);
            int rotation = Random.Range(0, 4);
            Instantiate(deathsplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));

            if (shouldDropItem)
            {
                float dropChance = Random.Range(0f, 100f);
                if (dropChance < itemDropPercent)
                {
                    int randomItem = Random.Range(0, itemToDrop.Length);
                    Instantiate(itemToDrop[randomItem], transform.position, transform.rotation);
                }
            }
        }
    }
}
