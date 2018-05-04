using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Slider volume;
    public Toggle mute;
    public AudioSource source;
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        //audioMixer.SetFloat("MasterVolume", volume);
        source.volume = volume;
        Debug.Log(volume);
        MuteGame();
    }

    public void MuteGame ()
    {
        if (mute.isOn)
        {
            source.volume = volume.minValue;
            //audioMixer.SetFloat("MasterVolume", volume.minValue);
        }
    }

    public void UnmuteGame ()
    {
        if (mute.isOn == false)
        {
            source.volume = volume.value;
            //audioMixer.SetFloat("MasterVolume", volume.value);
        }
    }
}
