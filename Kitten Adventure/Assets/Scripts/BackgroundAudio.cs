using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    private static BackgroundAudio instance = null;
    private AudioSource music;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    void Start()
    {        
        music = GetComponent<AudioSource>();
        music.Play();        
    }

     public void ChangeMusic(bool on)
    {
        if (on == true)
        {
            music.Play();
        }
        else
        {
            music.Stop();
        }
    }


}
