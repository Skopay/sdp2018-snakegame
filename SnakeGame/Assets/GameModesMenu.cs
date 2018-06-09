using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModesMenu : MonoBehaviour
{

    public Toggle classic;
    public Toggle arcade;
    public Toggle speedAttack;
    public static int gameMode;

    // Use this for initialization
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
