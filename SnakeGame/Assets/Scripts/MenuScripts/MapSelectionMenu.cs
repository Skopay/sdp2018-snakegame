using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectionMenu : MonoBehaviour {
	
    //Loads the classic map
	public void LoadClassicMap ()
    {
        SceneManager.LoadScene(1);
    }

    //Loads the borderless map
    public void LoadBorderlessMap()
    {
        SceneManager.LoadScene(2);
    }

    //Loads the obstacle map
    public void LoadObstacleMap()
    {
        SceneManager.LoadScene(3);
    }
}
