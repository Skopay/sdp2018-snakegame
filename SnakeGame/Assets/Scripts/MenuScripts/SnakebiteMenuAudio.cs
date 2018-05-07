using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakebiteMenuAudio : MonoBehaviour {

    //All audio objects in the menu
    public AudioSource source;
    public AudioClip buttonSelect;
    public AudioClip purchaseItem;
    public AudioClip gamemodeSelect;

    //PLays the audio for menu selection
    public void PlayMenuSelection ()
    {
        source.PlayOneShot(buttonSelect);
    }

    //Plays the audio for an item purchase
    public void PlayPurchaseItem ()
    {
        source.PlayOneShot(purchaseItem);
    }

    //PLays the audio for selecting a game mode
    public void PlayGamemodeSelect ()
    {
        source.PlayOneShot(gamemodeSelect);
    }
}
