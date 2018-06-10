using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour {

    public Text highScore;
    public Text previousScore;

	// Displays the highscore and previous score of the user
	void Start () {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        previousScore.text = PlayerPrefs.GetInt("PreviousScore").ToString();
	}

}
