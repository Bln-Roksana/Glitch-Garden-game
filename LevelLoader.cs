using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public int noOfSeconds = 5;
    int currentSceneNo;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneNo = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneNo == 0)
        {
            StartCoroutine(LoadStartScreen());
        }

    }

    IEnumerator LoadStartScreen()
    {
        yield return new WaitForSeconds(noOfSeconds);
        SceneManager.LoadScene(currentSceneNo + 1);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneNo);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void LoadNextLevel()
    {
        if (currentSceneNo == 1)
        {
            SceneManager.LoadScene(currentSceneNo + 2);
        }
        else
        {
            SceneManager.LoadScene(currentSceneNo + 1);
        }
    }

    public void LoadOptionScreen()
    {
        SceneManager.LoadScene("Option Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
