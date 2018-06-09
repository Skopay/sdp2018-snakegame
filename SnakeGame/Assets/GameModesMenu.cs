using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModesMenu : MonoBehaviour {

    public Toggle classic;
    public Toggle arcade;
    public Toggle speedAttack;

	// Use this for initialization
	void Start () {
		if (classic.isOn) {
            GameController.gameMode = 1;
        } else if (arcade.isOn)
        {
            GameController.gameMode = 2;
        } else if (speedAttack.isOn)
        {
            GameController.gameMode = 3;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
