using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakebiteMenu : MonoBehaviour
{
    public SettingsMenu settings;

    //Initialises the settings on startup
    public void Start()
    {
        InitialiseSettings();
    }

    //Loads the game scene
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    //Exits the application
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    //Sets the settings to their default values 
    public void InitialiseSettings ()
    {
        settings.Start();
        settings.SetDifficulty();
    }
}
