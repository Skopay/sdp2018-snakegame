using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    //All the game objects used within the game
    public GameObject foodPrefab;
    public GameObject currentFood;
    public GameObject powerup;
    public GameObject invincibilityPrefab;
    public GameObject randomPrefab;
    public GameObject increaseSpeedPrefab;
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
    public float powerupCountdownValue;
    public Text powerupTimer;
    //gamemode value, 1 = arcade, 2 = classic, 3 = speedattack
    public int gameMode = 2;
    public float arcadeSpeed;

    //Runs the Hit() script when hit is activated
    private void OnEnable()
    {
        Snake.hit += Hit;
    }

    // Use this for initialization. Constantly repeats the TimerInvoke() method
    void Start()
    {
        arcadeSpeed = PlayerPrefs.GetFloat("Speed");
        InvokeRepeating("TimerInvoke", 1, arcadeSpeed);
        FoodFunction();
        SpawnPowerup();

        //This is the code that'll decide if you're playing the normal gamemode or if you're playing "Speedattack", once this if statement
        //has been corrected, then you can uncomment 203 also which will be all the code needed for "Speedattack"
        /*if (gameMode == 1 ..... classic mode) {
            InvokeRepeating("TimerInvoke", 1, PlayerPrefs.GetFloat("Speed"));
        }
        else if (gameMode == 2 ..... arcade mode) {
        float arcadeSpeed = PlayerPrefs.GetFloat("Speed");
            InvokeRepeating("TimerInvoke", 1, arcadeSpeed);
        }
        else if (gameMode == 3) .... speedattack
        {
        InvokeRepeating("TimerInvoke", 1, deltaTimer;
        }*/
    }

    //Disables the Hit() script when it has finished running
    private void OnDisable()
    {
        Snake.hit -= Hit;
    }

    // Update is called once per frame. Checks for update on Snake's movement
    void Update()
    {
        KeyboardChangeDir();
        CheckWhatWasHit();
    }

    //Moves the Snake each frame by adding a 'head' in the chosen direction and removing the 'tail' as it moves.
    void TimerInvoke()
    {
        Movement();
        StartCoroutine(CheckVisable());
        if (currentSize >= maxSize)
        {
            TailFunction();
        }
        else
        {
            currentSize++;
        }
    }

    void CheckWhatWasHit()
    {
        if (!head.objectTag.Equals(""))
        {
            Hit(head.objectTag);
            head.objectTag = "";
        }
    }

    //Calculates the button pressed and moves the Snake accordingly
    void Movement()
    {
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
        if (NESW != 2 && Input.GetKeyDown(KeyCode.W))
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
        if (NESW != 3 && direction == 1)
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

    //Activates powerups in-game if Arcade gamemode is selected
    void SpawnPowerup()
    {
        string randomPowerupString = "";
        int randomPowerup = 0;
        if (gameMode == 2)
        {
            if (arcadeSpeed <= .10000f)
            {
                randomPowerup = Random.Range(1, 5);
            }
            else
            {
                randomPowerup = Random.Range(1, 7);
            }

            Debug.Log(arcadeSpeed);
            Debug.Log(randomPowerup);

            if (randomPowerup == 1 || randomPowerup == 3)
            {
                randomPowerupString = "invincibility";
            }
            else if (randomPowerup == 2 || randomPowerup == 4)
            {
                randomPowerupString = "random";
            }
            else if (randomPowerup == 5 || randomPowerup == 6)
            {
                randomPowerupString = "increaseSpeed";
            }
            Debug.Log(randomPowerupString);
            PowerupFunction(randomPowerupString);
        }
    }

    //Creates a new food object in the bounds of the camera
    void FoodFunction()
    {
        int xPos = 0;
        int yPos = 0;

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            do
            {
                xPos = Random.Range(-xBound, +xBound);
                yPos = Random.Range(-yBound, +yBound);

            } while ((yPos == -3 && xPos == 7) || (yPos == -3 && xPos == 8) || (yPos == -3 && xPos == 6) || (yPos == -3 && xPos == -7) || (yPos == -3 && xPos == -8) || (yPos == -3 && xPos == -6)
            || (yPos == -3 && xPos == 11) || (yPos == -3 && xPos == -11) || (yPos == -1 && xPos == 9) || (yPos == -1 && xPos == -9) || (yPos == -1 && xPos == 6) || (yPos == -1 && xPos == -6)
            || (yPos == -2 && xPos == 0) || (yPos == 1 && xPos == -7) || (yPos == 1 && xPos == 7) || (yPos == 2 && xPos == 3) || (yPos == 2 && xPos == -3) || (yPos == 2 && xPos == 10)
            || (yPos == 2 && xPos == -10) || (yPos == 0 && xPos == 3) || (yPos == 0 && xPos == -3) || (yPos == -4 && xPos == 0) || (xPos == -2 && yPos == 2) || (xPos == -4 && yPos == 2)
            || (yPos == 0 && xPos == 12) || (yPos == 0 && xPos == -12) || (xPos == -1 && yPos == -2) || (xPos == 1 && yPos == -2) || (xPos == 2 && yPos == 2) || (xPos == 4 && yPos == 2));
        }
        else
        {
            xPos = Random.Range(-xBound, +xBound);
            yPos = Random.Range(-yBound, +yBound);
        }

        Debug.Log("Food spawn");
        Debug.Log(xPos);
        Debug.Log(yPos);
        currentFood = (GameObject)Instantiate(foodPrefab, new Vector3(xPos, yPos, 90), transform.rotation);

    }


    void PowerupFunction(string whatPowerup)
    {
        int xPos = 0;
        int yPos = 0;

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            do
            {
                xPos = Random.Range(-xBound, +xBound);
                yPos = Random.Range(-yBound, +yBound);
            } while ((yPos == -3 && xPos == 7) || (yPos == -3 && xPos == 8) || (yPos == -3 && xPos == 6) || (yPos == -3 && xPos == -7) || (yPos == -3 && xPos == -8) || (yPos == -3 && xPos == -6)
            || (yPos == -3 && xPos == 11) || (yPos == -3 && xPos == -11) || (yPos == -1 && xPos == 9) || (yPos == -1 && xPos == -9) || (yPos == -1 && xPos == 6) || (yPos == -1 && xPos == -6)
            || (yPos == -2 && xPos == 0) || (yPos == 1 && xPos == -7) || (yPos == 1 && xPos == 7) || (yPos == 2 && xPos == 3) || (yPos == 2 && xPos == -3) || (yPos == 2 && xPos == 10)
            || (yPos == 2 && xPos == -10) || (yPos == 0 && xPos == 3) || (yPos == 0 && xPos == -3) || (yPos == -4 && xPos == 0) || (xPos == -2 && yPos == 2) || (xPos == -4 && yPos == 2)
            || (yPos == 0 && xPos == 12) || (yPos == 0 && xPos == -12) || (xPos == -1 && yPos == -2) || (xPos == 1 && yPos == -2) || (xPos == 2 && yPos == 2) || (xPos == 4 && yPos == 2));
        }
        else
        {
            xPos = Random.Range(-xBound, +xBound);
            yPos = Random.Range(-yBound, +yBound);
        }

        //spawns a randomised powerup
        if (whatPowerup == "invincibility")
        {
            powerup = (GameObject)Instantiate(invincibilityPrefab, new Vector3(xPos, yPos, 90), transform.rotation);
        }
        else if (whatPowerup == "increaseSpeed")
        {
            powerup = (GameObject)Instantiate(increaseSpeedPrefab, new Vector3(xPos, yPos, 90), transform.rotation);
        }
        else if (whatPowerup == "random")
        {
            powerup = (GameObject)Instantiate(randomPrefab, new Vector3(xPos, yPos, 90), transform.rotation);

        }
    }

    //Executes code depending on what object the Snake hit
    public void Hit(string whatWasSent)
    {
        if (whatWasSent == "Food")
        {
            //Increases Snake speed to a limit (used in "Speedattack mode"
            /*
             if (gameMode == 3){
             if (deltaTimer > .10000f)
            {
                deltaTime -= .05000;
                float haultMovement = deltaTimer;
                CancelInvoke("TimerInvoke");
                InvokeRepeating("TimerInvoke", haultMovement, deltaTimer);
            }
            }*/

            maxSize++;
            score++;
            scoreText.text = score.ToString();
            FoodFunction();
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
            StartCoroutine(InvincibilityCountdown());

        }
        else if (whatWasSent == "IncreaseSpeed")
        {
            float haultMovement = arcadeSpeed;
            arcadeSpeed -= (float).075;
            CancelInvoke("TimerInvoke");
            InvokeRepeating("TimerInvoke", haultMovement, arcadeSpeed);
            StartCoroutine(PowerupCooldown("increaseSpeed"));
        }
        else if (whatWasSent == "Random")
        {
            string random = "";

            int randomNumber = Random.Range(1, 6);
            if (randomNumber == 1 || randomNumber == 5)
            {
                random = "Invincibility";
            }
            else if (randomNumber == 2 || randomNumber == 4)
            {
                score = score + 5;
                scoreText.text = score.ToString();
                powerupTimer.text = "+5 SCORE!!!";
                StartCoroutine(PowerupCooldown("invincibility"));
                random = "";
            }
            else if (randomNumber == 3)
            {
                random = "Wall";
            }

            Debug.Log(random);
            Hit(random);
        }

        //Ends game if obstacle is hit
        else if (whatWasSent == "Snake")
        {
            //Idea for 'power up' code
            if (isInvincible == false)
            {
                CancelInvoke("TimerInvoke");
                SceneManager.LoadScene(0);
            }
        }
        else if (whatWasSent == "Wall")
        {
            CancelInvoke("TimerInvoke");
            SceneManager.LoadScene(0);
        }
    }

    public IEnumerator InvincibilityCountdown(float countdownValue = 10)
    {
        powerupTimer.text = "Invincibility: 10";
        powerupCountdownValue = countdownValue;
        while (powerupCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            powerupCountdownValue--;
            powerupTimer.text = "Invincibility: " + powerupCountdownValue.ToString();
        }
        isInvincible = false;
        yield return new WaitForSeconds(1.0f);
        powerupTimer.text = "";
        StartCoroutine(PowerupCooldown("invincibility"));

    }

    public IEnumerator PowerupCooldown(string powerupType)
    {
        if (powerupType == "invincibility")
        {
            yield return new WaitForSeconds(20.0f);
        }
        else if (powerupType == "increaseSpeed")
        {
            yield return new WaitForSeconds(10.0f);
        }
        SpawnPowerup();
    }


    //Two methods that manipulate the camera to mimick the Snake going through one side of the screen and coming out opposite side
    void Wrap()
    {
        if (NESW == 0)
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
    }
}