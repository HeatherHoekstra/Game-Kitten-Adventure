using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Sprite music;
    public Sprite noMusic;
    public Sprite sound;
    public Sprite noSound;

    [SerializeField] private Image musicImage;
    [SerializeField] private Image soundImage;
    [SerializeField] private AudioSource pickup;

    private static int musicOn = 0; //true
    private static int soundOn = 0; //true

    public BackgroundAudio musicAudio;
            

    private void Start()
    {
        soundOn = PlayerPrefs.GetInt("SoundOn");
        musicOn = PlayerPrefs.GetInt("MusicOn");

        if (soundOn == 0)
        {
            soundImage.sprite = sound;
        }
        else
        {
            soundImage.sprite = noSound;
        }

        if (musicOn == 0)
        {
            musicImage.sprite = music;
        }
        else
        {
            musicImage.sprite = noMusic;
        }
    }

    public void MusicButton()
    {        
        if (musicOn == 0)
        {
            musicImage.sprite = noMusic;                       
            musicOn = 1;
            PlayerPrefs.SetInt("MusicOn", musicOn);
            musicAudio.ChangeMusic();
        }
        else
        {            
            musicImage.sprite = music;
            musicOn = 0;
            PlayerPrefs.SetInt("MusicOn", musicOn);
            musicAudio.ChangeMusic();
        }

    }

    public void SoundButton()
    {
        if (soundOn == 0)
        {
            soundImage.sprite = noSound;            
            soundOn = 1;
            PlayerPrefs.SetInt("SoundOn", soundOn);
            
        }
        else
        {
            soundImage.sprite = sound;            
            soundOn = 0;
            PlayerPrefs.SetInt("SoundOn", soundOn);
            pickup.Play();
            
        }
    }


   
}
