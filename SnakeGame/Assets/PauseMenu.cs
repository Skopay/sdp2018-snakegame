using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;
    public Button restart;
    public Button resume;
    public Button exit;
    public static bool gameIsPaused = false;

    public void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Restart ()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit ()
    {
        SceneManager.LoadScene(0);
    }
}
