﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{

    public GunPickup[] potentialGun;

    public SpriteRenderer theSR;
    public Sprite chestOpen;

    public GameObject notification;

    private bool canOpen, isOpen;

    public Transform spawnPoint;

    public float scaleSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                int gunselect = Random.Range(0, potentialGun.Length);
                {
                    Instantiate(potentialGun[gunselect], spawnPoint.position, spawnPoint.rotation);
                    theSR.sprite = chestOpen;
                    isOpen = true;

                    transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                }
            }
        }
        if (isOpen)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            notification.SetActive(true);

            canOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            notification.SetActive(false);
            canOpen = false;
        }
    }


}
