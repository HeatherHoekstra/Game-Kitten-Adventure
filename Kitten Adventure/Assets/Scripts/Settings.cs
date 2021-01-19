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

    private bool musicOn = true;
    private bool soundOn = true;

    public BackgroundAudio musicAudio;
    public PlayerController soundAudio;
    
    public void MusicButton()
    {        
        if (musicOn)
        {
            musicImage.sprite = noMusic;                       
            musicOn = false;
            musicAudio.ChangeMusic(musicOn);
        }
        else
        {            
            musicImage.sprite = music;
            musicOn = true;
            musicAudio.ChangeMusic(musicOn);
                      
        }

    }

    public void SoundButton()
    {
        if (soundOn)
        {
            soundImage.sprite = noSound;            
            soundOn = false;
            soundAudio.ChangeSound(soundOn);
        }
        else
        {
            soundImage.sprite = sound;            
            soundOn = true;
            soundAudio.ChangeSound(soundOn);
            pickup.Play();
        }
    }

   
}
