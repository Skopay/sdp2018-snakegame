using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour {

    public Text highScore;

	// Use this for initialization
	void Start () {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
