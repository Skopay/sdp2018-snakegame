using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Controls the functionality of the pause menu
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; //Canvas of the pause menu
    public Button restart; //Restart button on the pause menu
    public Button resume; //Resume button on the pause menu
    public Button exit; //Exit button on the pause menu
    public static bool gameIsPaused = false; //Keeps track of whether the game is paused or not

    //Makes the pause menu appear when the pause button is selected
    public void Pause()
    {
        gameIsPaused = true;
        pauseMenuUI.SetActive(gameIsPaused);
        Time.timeScale = 0f; //Freezes time
    }

    //Resumes the current game
    public void Resume()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(gameIsPaused);
        Time.timeScale = 1.0f; //Sets time to normal speed
    }

    //Restarts the current game
    public void Restart()
    {
        Time.timeScale = 1.0f; //Sets time to normal speed
        CancelInvoke("TimerInvoke");
        SceneManager.LoadScene(1);
    }

    //Exits to the main menu
    public void Exit()
    {
        Time.timeScale = 1.0f; //Sets time to normal speed
        CancelInvoke("TimerInvoke");
        SceneManager.LoadScene(0);
    }
}

