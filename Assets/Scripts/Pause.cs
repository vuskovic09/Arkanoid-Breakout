using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;
    bool isPaused = false;

    public void pauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void unPauseGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
