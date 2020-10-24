using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{      
    public static LevelManager instance;
    public float waitToLoad =4f;
    public string nextLevel;
    public bool isPaused;

    public int currentCoins;

    public Transform startPoint;
    void Awake()
    {
        instance= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.transform.position = startPoint.position;
        PlayerController.instance.canMove = true;
        
        currentCoins = CharacterTracker.instance.currentCoins;
        Time.timeScale = 1f;
        UIController.instance.coinText.text = LevelManager.instance.currentCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause_Unpasue();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManger.instance.PlayLevelWin();
        PlayerController.instance.canMove = false;

        UIController.instance.startFadeToBlack();
        yield return new WaitForSeconds(waitToLoad);
        CharacterTracker.instance.currentCoins = currentCoins;
        CharacterTracker.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        CharacterTracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;
        SceneManager.LoadScene(nextLevel);
    }

    public void Pause_Unpasue()
    {
        if(!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);
            isPaused = true;

            Time.timeScale =0f;
        }else
        {
            UIController.instance.pauseMenu.SetActive(false);
            isPaused = false;

            Time.timeScale = 1f;
        }
    }
    public void GetCoins(int amount)
    {
        currentCoins += amount;
        UIController.instance.coinText.text = LevelManager.instance.currentCoins.ToString();
    }

    public void SpendCoins(int amount)
    {
        currentCoins -= amount;

        if(currentCoins < 0)
        {
            currentCoins = 0;
        }
        UIController.instance.coinText.text = LevelManager.instance.currentCoins.ToString();
    }
}
