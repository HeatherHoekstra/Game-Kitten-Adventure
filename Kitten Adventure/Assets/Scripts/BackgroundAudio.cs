using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
   // private static BackgroundAudio instance = null;
    private AudioSource music;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //        return;
    //    }
    //    if (instance == this) return;
    //    Destroy(gameObject);

        
    //}

    void Start()
    {  
        PlayerPrefs.GetInt("MusicOn");      
        music = GetComponent<AudioSource>();

        if(PlayerPrefs.GetInt("MusicOn") == 0) { 
        music.Play();
        }
    }

    public void ChangeMusic()
    {
        if(PlayerPrefs.GetInt("MusicOn") == 0)
        {
            music.Play();
        }
        else
        {
            music.Stop();
        }
    }



}
