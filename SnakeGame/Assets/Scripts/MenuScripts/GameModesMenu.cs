using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModesMenu : MonoBehaviour
{
    //Toggles controlling the game type selected
    public Toggle classic;
    public Toggle arcade;
    public Toggle speedAttack;
    
    //Reference to certain game mode
    public static int gameMode;

    // Updates which game mode is selected
    void Update()
    {

        if (classic.isOn)
        {
            gameMode = 1;
        }
        else if (arcade.isOn)
        {
            gameMode = 2;
        }
        else if (speedAttack.isOn)
        {
            gameMode = 3;
        }
        else
        {
            gameMode = 0;
        }
    }
}
