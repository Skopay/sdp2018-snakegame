using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeInfo : MonoBehaviour {

    public GameObject arcadeInfo;

	// Use this for initialization
	void Start () {
		if(GameController.gameMode == 2)
        {
            Time.timeScale = 0f;
            arcadeInfo.SetActive(true);
        }
	}

    public void Continue ()
    {
        Time.timeScale = 1.0f;
        arcadeInfo.SetActive(false);
    }
}
