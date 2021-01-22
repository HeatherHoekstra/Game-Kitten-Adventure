using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int currentLevel;

    public void Play()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if(currentLevel > 1)
        {
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }        
    }
    
}
