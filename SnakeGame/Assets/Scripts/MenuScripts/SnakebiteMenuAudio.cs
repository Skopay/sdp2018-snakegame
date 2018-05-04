using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakebiteMenuAudio : MonoBehaviour {

    public AudioSource source;
    public AudioClip buttonSelect;
    public AudioClip purchaseItem;
    public AudioClip gamemodeSelect;

    public void PlayMenuSelection ()
    {
        source.PlayOneShot(buttonSelect);
    }

    public void PlayPurchaseItem ()
    {
        source.PlayOneShot(purchaseItem)
;    }

    public void PlayGamemodeSelect ()
    {
        source.PlayOneShot(gamemodeSelect);
    }
}
