using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnakebiteMenu : MonoBehaviour
{
    public SettingsMenu settings;
    public Text hS;

    //Initialises the settings on startup
    public void Start()
    {
        InitialiseSettings();
        PlayerPrefs.GetFloat("Speed");
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

    void HSFunction()
    {
        hS.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
