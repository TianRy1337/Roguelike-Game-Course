using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;
    public GameObject impactEffect;
    public int damageToGive = 50;


    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        AudioManger.instance.PlaySFX(4);
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }
        if (other.tag == "Boss")
        {
            BossController.instance.TakeDemage(damageToGive);
            Instantiate(BossController.instance.hitEffect, transform.position, transform.rotation);
        }
    }


    private void OnBecameInvisible()
    {
        //Instantiate(impactEffect,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
