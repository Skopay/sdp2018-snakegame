using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int xBound;
    public int yBound;
    public Snake head;
    public Snake tail;
    public GameObject foodPrefab;
    public GameObject currentFood;
    public GameObject snakePrefab;
    public GameObject wallNorth;
    public GameObject wallSouth;
    public GameObject wallEast;
    public GameObject wallWest;
    public int maxSize;
    public int currentSize;
    public int NESW;
    public int score;
    public float deltaTimer;
    public Vector2 newPos;
    public Text scoreText;

    private void OnEnable()
    {
        Snake.hit += hit;
    }
    // Use this for initialization
    void Start () {
        InvokeRepeating("TimerInvoke", 0, deltaTimer);
        FoodFunction();
	}

    private void OnDisable()
    {
        Snake.hit -= hit;
    }

    // Update is called once per frame
    void Update () {
        ComChangeD();
	}

    void TimerInvoke()
    {
        Movement();
        StartCoroutine(CheckVisable());
        if(currentSize >= maxSize)
        {
            TailFunction();
        }else
        {
            currentSize++;
            
        }
    }

    void Movement()
    {
        GameObject temp;
        newPos = head.transform.position;

        switch (NESW)
        {
            case 0:
                newPos = new Vector2(newPos.x, newPos.y + 1);
                break;
            case 1:
                newPos = new Vector2(newPos.x + 1, newPos.y);
                break;
            case 2:
                newPos = new Vector2(newPos.x, newPos.y - 1);
                break;
            case 3:
                newPos = new Vector2(newPos.x - 1, newPos.y);
                break;
        }
        temp = (GameObject)Instantiate(snakePrefab, newPos, transform.rotation);
        head.SetNext(temp.GetComponent<Snake>());
        head = temp.GetComponent<Snake>();

        return;
    }


    void ComChangeD()
    {
        if(NESW != 2 && Input.GetKeyDown(KeyCode.W))
        {
            NESW = 0;
        }
        if (NESW != 3 && Input.GetKeyDown(KeyCode.D))
        {
            NESW = 1;
        }
        if (NESW != 0 && Input.GetKeyDown(KeyCode.S))
        {
            NESW = 2;
        }
        if (NESW != 1 && Input.GetKeyDown(KeyCode.A))
        {
            NESW = 3;
        }
    }

    public void MobChangeD(int direction)
    {
        if (NESW != 2 && direction == 0)
        {
            NESW = 0;
        }
        if (NESW != 3 && direction == 1 )
        {
            NESW = 1;
        }
        if (NESW != 0 && direction == 2)
        {
            NESW = 2;
        }
        if (NESW != 1 && direction == 3)
        {
            NESW = 3;
        }
    }

    void TailFunction()
    {
        Snake tempSnake = tail;
        tail = tail.GetNext();
        tempSnake.RemoveTail();
    }

    void FoodFunction()
    {
        int xPos = Random.Range(-xBound, +xBound);
        int yPos = Random.Range(-yBound, +yBound);

        currentFood = (GameObject)Instantiate(foodPrefab, new Vector2(xPos, yPos), transform.rotation);

        //StartCoroutine(CheckRender(currentFood));
    }

    //not needed, i think
    /*IEnumerator CheckRender(GameObject IN)
    {
        yield return new WaitForEndOfFrame();
        if(IN.GetComponent<Renderer>().isVisible == false)
        {
            if(IN.tag == "Food")
            {
                Destroy(IN);
                FoodFunction();
            }
        }
    }*/

    void hit(string WhatWasSent)
    {
        if(WhatWasSent == "Food")
        {
            if (deltaTimer > .10000f)
            {
                deltaTimer -= .0125000f;
                CancelInvoke("TimerInvoke");
                InvokeRepeating("TimerInvoke", 0, deltaTimer);
            }
            FoodFunction();
            maxSize++;
            score++;
            scoreText.text = score.ToString();
            int temp = PlayerPrefs.GetInt("HighScore");
            if(score > temp)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        if(WhatWasSent == "Snake" || WhatWasSent == "Wall")
        {
            /*if (powerUpInvis == true)
            {
                return;
                
            }*/
            CancelInvoke("TimerInvoke");
            Exit();
        }
    }

    //makes it boundless, other map idea
    /*void Wrap()
    {
        if(NESW == 0)
        {
            head.transform.position = new Vector2(head.transform.position.x, -(head.transform.position.y - 1));
        }
        if (NESW == 1)
        {
            head.transform.position = new Vector2(-(head.transform.position.x - 1), head.transform.position.y);
        }
        if (NESW == 2)
        {
            head.transform.position = new Vector2(head.transform.position.x, -(head.transform.position.y + 1));
        }
        if (NESW == 3)
        {
            head.transform.position = new Vector2(-(head.transform.position.x + 1), head.transform.position.y);
        }
    }*/

    IEnumerator CheckVisable()
    {
        yield return new WaitForEndOfFrame();
        if (!head.GetComponent<Renderer>().isVisible)
        {
            CancelInvoke("TimerInvoke");
            Exit();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
