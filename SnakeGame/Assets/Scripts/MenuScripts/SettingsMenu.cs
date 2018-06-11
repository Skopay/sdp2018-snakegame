using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    //All objects used within settings menu
    public Slider volume;
    public Slider difficulty;
    public Toggle mute;
    public AudioSource source;

    //Sets default values for settings
    public void Start()
    {
        difficulty.value = PlayerPrefs.GetFloat("Speed");
        volume.value = 5.0f;
    }

    //Uses value of slider to set the volume
    public void SetVolume(float volume)
    {
        source.volume = volume;
        MuteGame();
    }

    //Mutes the game if mute box is checked
    public void MuteGame()
    {
        if (mute.isOn)
        {
            source.volume = volume.minValue;
        }
    }

    //Unmutes the game if mute box isn't checked
    public void UnmuteGame()
    {
        if (mute.isOn == false)
        {
            source.volume = volume.value;
        }
    }

    //Sets the speed of the sanke within the game
    public void SetDifficulty()
    {
        GameController.deltaTimer = difficulty.value;
        PlayerPrefs.SetFloat("Speed", GameController.deltaTimer);
    }
}
