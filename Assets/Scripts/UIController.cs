using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider healthSlider;
    public Text healText;
    public Text coinText;

    public GameObject deathScreen;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack,fadeOutBlack;
    public string newGameScene,mainMenuScene;

    public GameObject pauseMenu,mapDisplay,bigMapText;

    public Image currentGun;
    public Text gunText;


    public Slider bossHealthBar;

    void Awake()
    {   
      instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
      //這裡新增
      /*////////////////////////////////*/
      healthSlider.maxValue = CharacterTracker.instance.maxHealth;
      healthSlider.value = CharacterTracker.instance.currentHealth;
      healText.text = PlayerHealthController.instance.currentHealth.ToString()+" / " + PlayerHealthController.instance.maxHealth.ToString();
      coinText.text = CharacterTracker.instance.currentCoins.ToString();
      /*////////////////////////////////*/

      PlayerHealthController.instance.maxHealth = CharacterTracker.instance.maxHealth;
      PlayerHealthController.instance.currentHealth = CharacterTracker.instance.currentHealth;
      //coinText = CharacterTracker.instance.currentCoins;

      fadeOutBlack = true;
      fadeToBlack = false;
      currentGun.sprite = PlayerController.instance.availableGuns[PlayerController.instance.currentGun].gunUI;
      gunText.text = PlayerController.instance.availableGuns[PlayerController.instance.currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutBlack)
        {
          fadeScreen.color = new Color (fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a, 0f,fadeSpeed*Time.deltaTime));
          if(fadeScreen.color.a == 0f)
          {
            fadeOutBlack = false;
          }
        }

        if(fadeToBlack)
        {
          fadeScreen.color = new Color (fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a, 1f,fadeSpeed*Time.deltaTime));
          if(fadeScreen.color.a == 1f)
          {
            fadeToBlack = false;
          }
        }
    }
  public void startFadeToBlack()
  {
    fadeToBlack = true;
    fadeOutBlack = false;
  }
  public void NewGame()
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene(newGameScene);
    Destroy(PlayerController.instance.gameObject);
  }
  public void ReturnMainMenu()
  {
    Time.timeScale = 1f;
    SceneManager.LoadScene(mainMenuScene);
    Destroy(PlayerController.instance.gameObject);
  }

  public void Resume()
  {
    LevelManager.instance.Pause_Unpasue();
  }
}



