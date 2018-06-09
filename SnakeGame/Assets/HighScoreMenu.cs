using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour {

    public Text highScore;
    public Text previousScore;

	// Use this for initialization
	void Start () {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        previousScore.text = PlayerPrefs.GetInt("PreviousScore").ToString();
	}

}
