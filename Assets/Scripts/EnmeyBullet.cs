using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyBullet : MonoBehaviour
{

    public float speed;
    private Vector3 direction;
    public int bulletDamage;


    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    
   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(bulletDamage);
        }
        Destroy(gameObject);
        AudioManger.instance.PlaySFX(4);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
