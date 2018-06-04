using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Controls the functionality of the pause menu
public class PauseMenu : MonoBehaviour
{
    public GameController controller = new GameController();
    public GameObject pauseMenuUI; //Canvas of the pause menu
    public Button restart; //Restart button on the pause menu
    public Button resume; //Resume button on the pause menu
    public Button exit; //Exit button on the pause menu
    public static bool gameIsPaused = false; //Keeps track of whether the game is paused or not

    //Makes the pause menu appear when the pause button is selected
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //Freezes time
        gameIsPaused = true;
    }

    //Resumes the current game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f; //Sets time to normal speed
        gameIsPaused = false;
    }

    //Restarts the current game
    public void Restart()
    {
        //controller.RestartGame();
        SceneManager.LoadScene(2);
    }

    //Exits to the main menu
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}

