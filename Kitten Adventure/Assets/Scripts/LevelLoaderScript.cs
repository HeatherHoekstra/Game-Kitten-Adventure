using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{

    private int currentLevel;

    public Animator transition;
    public float transitionTime = 1f;
    public float waitTime = 3f;

    private void Start()
    {
       currentLevel = PlayerPrefs.GetInt("CurrentLevel");  
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {

        currentLevel += 1;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        yield return new WaitForSeconds(waitTime);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
        
    }
}
