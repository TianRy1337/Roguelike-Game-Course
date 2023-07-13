using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    public static AudioManger instance;
    public AudioSource level1Music, gameOverMusic, winMusic;
    public AudioSource[] Sfx;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayGameOver()
    {
        level1Music.Stop();
        gameOverMusic.Play();
    }
    public void PlayLevelWin()
    {
        level1Music.Stop();
        winMusic.Play();
    }
    public void PlaySFX(int sfxToPlay)
    {
        Sfx[sfxToPlay].Stop();
        Sfx[sfxToPlay].Play();
    }
}
