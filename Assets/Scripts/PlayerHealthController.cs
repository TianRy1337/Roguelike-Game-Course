using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance ;
    public int currentHealth;
    public int maxHealth;

    public float damageInvincLength = 1f;
    private float invincCount;

    void Awake()
    {
        instance = this;
    }
    



    void Start()
    {

        //這裡有改
        /*////////////////////////////////*/
        maxHealth = CharacterTracker.instance.maxHealth;
        currentHealth = CharacterTracker.instance.currentHealth;
        /*////////////////////////////////*/

        //currentHealth = maxHealth;

        
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healText.text = currentHealth.ToString()+" / " + maxHealth.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
         if(invincCount > 0)
         {
             invincCount -= Time.deltaTime;
             if(invincCount<=0)
             {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r,PlayerController.instance.bodySR.color.g,PlayerController.instance.bodySR.color.b,1f);
             }
         }
    }
    public void DamagePlayer(int damage)
    {
        if(invincCount <= 0)
        {
            AudioManger.instance.PlaySFX(11);
            currentHealth -=damage;

            invincCount = damageInvincLength;

            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r,PlayerController.instance.bodySR.color.g,PlayerController.instance.bodySR.color.b,.5f);

            if(currentHealth<=0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
                AudioManger.instance.PlayGameOver();
                AudioManger.instance.PlaySFX(8);
            }
            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healText.text = currentHealth.ToString()+" / " + maxHealth.ToString();
        }
    }
    public void MakeInvincible(float length)
    {
        invincCount = length;
        PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r,PlayerController.instance.bodySR.color.g,PlayerController.instance.bodySR.color.b,.5f);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healText.text = currentHealth.ToString()+" / " + maxHealth.ToString();
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healText.text = currentHealth.ToString()+" / " + maxHealth.ToString();
    }
}
