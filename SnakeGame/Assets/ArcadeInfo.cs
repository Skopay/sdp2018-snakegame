using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeInfo : MonoBehaviour {

    public GameObject arcadeInfo;

	//Checks if arcade mode is active and displays the info screen
	void Update() {
		if(GameModesMenu.gameMode == 2)
        {
            Time.timeScale = 0f;
            arcadeInfo.SetActive(true);
            Debug.Log("activated");
        }
	}

    //Begins the game
    public void Continue ()
    {
        Time.timeScale = 1.0f;
        arcadeInfo.SetActive(false);
        Debug.Log("activatedsdsads");
    }
}
