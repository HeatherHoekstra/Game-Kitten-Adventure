using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   
    public static bool gamePaused = false;

    public GameObject pauseMenu;
    public GameObject settingsCanvas;

    //private static PauseMenu instance = null;


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

    private void Awake()
    {
        settingsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseHandler();
        }
    }

    public void PauseHandler()
    {
        if (gamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingsCanvas.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        settingsCanvas.SetActive(true);
        Time.timeScale = 0f;        
        gamePaused = true;
    }

    public void MainMenuButton()
    {
        PauseHandler();
        SceneManager.LoadScene(0);        
    }
}
