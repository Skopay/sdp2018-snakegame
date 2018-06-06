using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //All the game objects used within the game
    public GameObject foodPrefab;
    public GameObject currentFood;
    public GameObject powerup;
    public GameObject powerupPrefab;
    public GameObject snakePrefab;
    public Snake head;
    public Snake tail;

    //Public variables that'll help with the algorithm of the game
    //Bounds of which the food will spawn
    public int xBound;
    public int yBound;
    //Increases the size of the Snake
    public int maxSize;
    public int currentSize;
    //Holds the value of the Snake's movement
    public int NESW;
    //Used to display the current score on the UI and store the value
    public int score;
    public Text scoreText;
    //Custom value for calculating the Snake's speed
    public static float deltaTimer;
    //Used for placing the Food and Snake object in the correct X and Y coordinate
    public Vector3 newPos;
    //Powerups
    public bool isInvincible;


    //Runs the Hit() script when hit is activated
    private void OnEnable()
    {
        Snake.hit += Hit;
    }
    
    // Use this for initialization. Constantly repeats the TimerInvoke() method
    void Start () {
        FoodFunction("Food");
        InvokeRepeating("TimerInvoke", 1, PlayerPrefs.GetFloat("Speed"));
    }

    //Disables the Hit() script when it has finished running
    private void OnDisable()
    {
        Snake.hit -= Hit;
    }

    // Update is called once per frame. Checks for update on Snake's movement
    void Update () {
        KeyboardChangeDir();
        CheckWhatWasHit();
    }

    //Moves the Snake each frame by adding a 'head' in the chosen direction and removing the 'tail' as it moves.
    void TimerInvoke()
    {
        Movement();
        //StartCoroutine(CheckVisable());
        if(currentSize >= maxSize)
        {
            TailFunction();
        }else
        {
            currentSize++;
        }
    }

    void CheckWhatWasHit()
    {
        if (!head.objectTag.Equals(""))
        {
            Hit(head.objectTag);
        }
        FoodFunction(head.objectTag);
        //insert powerup creation and put 15 second timer
        head.objectTag = "";
    }

    //Calculates the button pressed and moves the Snake accordingly
    void Movement() {
        GameObject temp;
        newPos = head.transform.position;

        switch (NESW)
        {
            case 0:
                newPos = new Vector3(newPos.x, newPos.y + 1, 90);
                break;
            case 1:
                newPos = new Vector3(newPos.x + 1, newPos.y, 90);
                break;
            case 2:
                newPos = new Vector3(newPos.x, newPos.y - 1, 90);
                break;
            case 3:
                newPos = new Vector3(newPos.x - 1, newPos.y, 90);
                break;
        }
        temp = (GameObject)Instantiate(snakePrefab, newPos, transform.rotation);
        head.SetNext(temp.GetComponent<Snake>());
        head = temp.GetComponent<Snake>();

        return;
    }

    //Movement method for keyboard based devices
    void KeyboardChangeDir()
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

    //Movement method for mobile devices
    public void MobileChangeDir(int direction)
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

    //Moves the tail of the Snake.
    void TailFunction()
    {
        Snake tempSnake = tail;
        tail = tail.GetNext();
        tempSnake.RemoveTail();
    }

    //Creates a new food object in the bounds of the camera
    void FoodFunction(string isFood)
    {
        if (isFood == "Food")
        {
            int xPos = Random.Range(-xBound, +xBound);
            int yPos = Random.Range(-yBound, +yBound);

            currentFood = (GameObject)Instantiate(foodPrefab, new Vector3(xPos, yPos, 90), transform.rotation);
        }
    }

    void PowerupFunction(string isPowerup)
    {
        if (isPowerup == "Invincibility")
        {
            int xPos = Random.Range(-xBound, +xBound);
            int yPos = Random.Range(-yBound, +yBound);

            powerup = (GameObject)Instantiate(powerupPrefab, new Vector3(xPos, yPos, 90), transform.rotation);
        }
    }

        //Executes code depending on what object the Snake hit
        public void Hit(string whatWasSent)
    {
        if (whatWasSent == "Food")
        {
            //Increases Snake speed to a limit (used in "TimeAttack mode"
            /*if (deltaTimer > .10000f)
            {
                float haultMovement = deltaTimer;
                CancelInvoke("TimerInvoke");
                InvokeRepeating("TimerInvoke", haultMovement, deltaTimer);
            }*/
            maxSize++;
            score++;
            scoreText.text = score.ToString();
            //Compares the current highscore to the current score and updates it if there is a change
            int temp = PlayerPrefs.GetInt("HighScore");
            if (score > temp)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else if (whatWasSent == "Invincibility")
        {
            
            isInvincible = true;
            //count a timer for 5 seconds on screen
            isInvincible = false;
        }

        //Ends game if obstacle is hit
        else if (whatWasSent == "Snake")
        {
            //Idea for 'power up' code
            if(isInvincible == false)
            {
                CancelInvoke("TimerInvoke");
                Exit();
            }
        }
        else if (whatWasSent == "Wall")
        {
            CancelInvoke("TimerInvoke");
            Exit();
        }
    }

    //Upon clicking the exit button you will be returned to the Main Menu
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    /*public void RestartGame()
    {
        FoodFunction("Food");
        InvokeRepeating("TimerInvoke", 1, PlayerPrefs.GetFloat("Speed"));
        Debug.Log(eastWall.transform.position.z);
        Debug.Log("Speed" + PlayerPrefs.GetFloat("Speed"));
    }*/

    //Two methods that manipulate the camera to mimick the Snake going through one side of the screen and coming out opposite side.
    /*void Wrap()
    {
        if(NESW == 0)
        {
            head.transform.position = new Vector2(tail.transform.position.x, -(head.transform.position.y - 1));
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
    }
    
    IEnumerator CheckVisable()
    {
        yield return new WaitForEndOfFrame();
        if (!head.GetComponent<Renderer>().isVisible)
        {
            Wrap();
        }
    }*/
}
