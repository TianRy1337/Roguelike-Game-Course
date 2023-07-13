using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject deletePanel;
    public CharacterSelector[] charactersToDelete;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void DeletSave()
    {
        deletePanel.SetActive(true);
    }
    public void ConfirmDelete()
    {
        deletePanel.SetActive(false);
        foreach (CharacterSelector theChar in charactersToDelete)
        {
            PlayerPrefs.SetInt(theChar.playerToSpawn.name, 0);
        }

    }
    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }
}
