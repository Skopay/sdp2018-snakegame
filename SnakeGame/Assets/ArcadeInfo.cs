using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeInfo : MonoBehaviour {

    public GameObject arcadeInfo;

	// Use this for initialization
	void Update() {
		if(GameModesMenu.gameMode == 2)
        {
            Time.timeScale = 0f;
            arcadeInfo.SetActive(true);
            Debug.Log("activated");
        }
	}

    public void Continue ()
    {
        Time.timeScale = 1.0f;
        arcadeInfo.SetActive(false);
        Debug.Log("activatedsdsads");
    }
}
