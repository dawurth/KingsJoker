using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Player player;
    public Workbench bench;
    public ItemManager itemManager;

    public Camera cam;

    // UI Elements
    GameObject Pickup;
    GameObject Place;
    GameObject Perform;
    GameObject Instructions;
    public GameObject holdingGO;
    public GameObject WorkBenchGO;
    public GameObject LaughBarGO;
    public GameObject TimerGO;
    public GameObject KingGO;


    // Temp sprites until I can get it loading correctly.
    public Sprite banana;
    public Sprite feather;
    public Sprite flower;
    public Sprite rabbit;
    public Sprite rubberduck;
    public Sprite fakebeard;
    public Sprite whoopee;
    public Sprite wig;
    public Sprite soap;
    public Sprite balloonanimal;

    public Sprite king; // 5-10
    public Sprite kingUpset; // 0-5
    public Sprite kingAngry; // <0
    public Sprite kingHappy; // 10-15
    public Sprite kingLove; // 15-20

    bool gameWon = false;
    GameObject collidingObject;
    // Start is called before the first frame update
    void Start()
    {
        player = new Player();
        bench = new Workbench();
        Pickup = GameObject.Find("Pickup");
        Pickup.SetActive(false);
        Place = GameObject.Find("PlaceItem");
        Place.SetActive(false);
        Perform = GameObject.Find("PerformAct");
        Perform.SetActive(false);
        Instructions = GameObject.Find("Instructions");
        Instructions.SetActive(true);
        itemManager = new ItemManager();
    }

    // Update is called once per frame
    void Update()
    {
        Timer time = TimerGO.GetComponent<Timer>();

        if (time.timerRunning)
        {
            // move the player in a direction.
            movePlayer();
            //if the player presses e, if there is a colliding object it should be picked up.
            getItem();
            // If the player presses e at a workbench while using the item it should be placed on the workbench
            useItem();
        }
        // if the player is at a workbench with 5 items and presses p the player should perform
        perform();
 
    
        // check if the player has reached 100 or higher and win the game.
        if (LaughBarGO.GetComponent<LaughBarController>().laughBar.value >= 100 && !gameWon)
        {
            gameWon = true;
            // Win the game
            Debug.Log("You win");
            
            time.stopTimer();
            float highScore = 0;

            float newScore = time.playerScore();
            PlayerPrefs.SetFloat("currentScore", newScore);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey("highScore")) {
                if (newScore < PlayerPrefs.GetFloat("highScore") || PlayerPrefs.GetFloat("highScore") == 0)
                {
                    highScore = newScore;
                    PlayerPrefs.SetFloat("highScore", highScore);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                if (newScore < highScore || highScore == 0)
                {
                    highScore = newScore;
                    PlayerPrefs.SetFloat("highScore", highScore);
                    PlayerPrefs.Save();
                }
            }

            StartCoroutine(TransferToWinScreen());
        }
    }

    IEnumerator FinishRound()
    {
        yield return new WaitForSeconds(5);
        bench.clearItems();
        // clear all items from the WorkBenchGo as well
        GameObject placedItem = WorkBenchGO.transform.Find("Item1").gameObject;
        placedItem.GetComponent<SpriteRenderer>().enabled = false;
        placedItem.GetComponent<GoRotation>().enabled = false;

        placedItem = WorkBenchGO.transform.Find("Item2").gameObject;
        placedItem.GetComponent<SpriteRenderer>().enabled = false;
        placedItem.GetComponent<GoRotation>().enabled = false;


        placedItem = WorkBenchGO.transform.Find("Item3").gameObject;
        placedItem.GetComponent<SpriteRenderer>().enabled = false;
        placedItem.GetComponent<GoRotation>().enabled = false;


        placedItem = WorkBenchGO.transform.Find("Item4").gameObject;
        placedItem.GetComponent<SpriteRenderer>().enabled = false;
        placedItem.GetComponent<GoRotation>().enabled = false;


        placedItem = WorkBenchGO.transform.Find("Item5").gameObject;
        placedItem.GetComponent<SpriteRenderer>().enabled = false;
        placedItem.GetComponent<GoRotation>().enabled = false;

        // after the performance we will need to respawn all the items.
        // how do we let the world controller know to respawn
        GameObject worldController = GameObject.Find("WorldController");
        worldController.GetComponent<WorldController>().spawnItems();

        // reset the timer
        Timer time = TimerGO.GetComponent<Timer>();
        time.startTimer();

    }

    IEnumerator TransferToWinScreen()
    {

        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Congrats");
    }

    void perform()
    {
        if (Input.GetKeyDown(KeyCode.P) && collidingObject != null && collidingObject.name == "Workbench" && bench.hasAllItems())
        {
            Timer time = TimerGO.GetComponent<Timer>();
            time.stopTimer();

            int pointsLoss = -(int)(time.timeTaken() / 10);
            // Id love if I could get all 5 items to circle around the player and then disappear
            // TODO All items circle the player
            // based on the items selected and if the king likes them, a final total will be added to the bar, once it hits 100 you win, if it hits 0 you die
            int moodResult = bench.calculateMood() + pointsLoss;

            Debug.Log("King's Mood Changed: " + moodResult);
            // Update the king's protrait based on the number of points
            if (moodResult >= 15)
            {
                KingGO.GetComponent<Image>().sprite = kingLove;
            }
            else if (moodResult >= 10)
            {
                KingGO.GetComponent<Image>().sprite = kingHappy;
            }
            else if (moodResult >= 5)
            {
                KingGO.GetComponent<Image>().sprite = king;
            }
            else if (moodResult >= 0)
            {
                KingGO.GetComponent<Image>().sprite = kingUpset;
            }
            else
            {
                KingGO.GetComponent<Image>().sprite = kingAngry;
            }

            LaughBarGO.GetComponent<LaughBarController>().setMood(moodResult);

            GameObject placedItem = WorkBenchGO.transform.Find("Item1").gameObject;
            placedItem.GetComponent<GoRotation>().enabled = true;

            placedItem = WorkBenchGO.transform.Find("Item2").gameObject;
            placedItem.GetComponent<GoRotation>().enabled = true;

            placedItem = WorkBenchGO.transform.Find("Item3").gameObject;
            placedItem.GetComponent<GoRotation>().enabled = true;

            placedItem = WorkBenchGO.transform.Find("Item4").gameObject;
            placedItem.GetComponent<GoRotation>().enabled = true;

            placedItem = WorkBenchGO.transform.Find("Item5").gameObject;
            placedItem.GetComponent<GoRotation>().enabled = true;

            StartCoroutine(FinishRound());
        }
    }

  
    // If the player is able to interact with a bench the item can be used at, if the key is pressed remove the item from the player, and add to the workbench
    // if the workbench has all items then the player will get an option to preform
    void useItem()
    {
        if (Input.GetKeyDown(KeyCode.R) && collidingObject != null && player.hasItem() && collidingObject.name == "Workbench")
        {   
            if(Instructions.activeSelf) {
                Instructions.SetActive(false);
            }
            Item heldItem = player.getItem();
            bench.addItem(heldItem);
            player.removeHeldItem();
            holdingGO.SetActive(false);
            // work out which image is next and update the sprite and set it active
            int itemCount = bench.getItemCount();
            Debug.Log("Item count: " + itemCount);
            GameObject placedItem = WorkBenchGO.transform.Find("Item" + itemCount).gameObject;
            Debug.Log(placedItem.name);
            placedItem.GetComponent<SpriteRenderer>().enabled = true;
            switch (heldItem.prefab)
            {
                case "rabbit":
                    placedItem.GetComponent<SpriteRenderer>().sprite = rabbit;
                    break;
                case "banana":
                    placedItem.GetComponent<SpriteRenderer>().sprite = banana;
                    break;
                case "flower":
                    placedItem.GetComponent<SpriteRenderer>().sprite = flower;
                    break;
                case "rubberduck":
                    placedItem.GetComponent<SpriteRenderer>().sprite = rubberduck;
                    break;
                case "feather":
                    placedItem.GetComponent<SpriteRenderer>().sprite = feather;
                    break;
                case "fakebeard":
                    placedItem.GetComponent<SpriteRenderer>().sprite = fakebeard;
                    break;
                case "whoopee":
                    placedItem.GetComponent<SpriteRenderer>().sprite = whoopee;
                    break;
                case "wig":
                    placedItem.GetComponent<SpriteRenderer>().sprite = wig;
                    break;
                case "soap":
                    placedItem.GetComponent<SpriteRenderer>().sprite = soap;
                    break;
                case "balloonanimal":
                    placedItem.GetComponent<SpriteRenderer>().sprite = balloonanimal;
                    break;
            }

            Place.SetActive(false);

            if(bench.hasAllItems()) {
                Perform.SetActive(true);
            }

        }
    }

    void getItem()
    {
        if (Input.GetKeyDown(KeyCode.E) && collidingObject != null && player.hasItem() == false)
        {
            Debug.Log(collidingObject.name);
            Item item = itemManager.getItem(collidingObject.name);
            Debug.Log(item.name);
            player.addItem(item);
            collidingObject.SetActive(false);
            collidingObject = null;

            holdingGO.SetActive(true);
            holdingGO.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
            switch (item.prefab)
            {
                case "rabbit":
                    holdingGO.GetComponentInChildren<Image>().sprite = rabbit;
                    break;
                case "banana":
                    holdingGO.GetComponentInChildren<Image>().sprite = banana;
                    break;
                case "flower":
                    holdingGO.GetComponentInChildren<Image>().sprite = flower;
                    break;
                case "rubberduck":
                    holdingGO.GetComponentInChildren<Image>().sprite = rubberduck;
                    break;
                case "feather":
                    holdingGO.GetComponentInChildren<Image>().sprite = feather;
                    break;
                case "fakebeard":
                    holdingGO.GetComponentInChildren<Image>().sprite = fakebeard;
                    break;
                case "whoopee":
                    holdingGO.GetComponentInChildren<Image>().sprite = whoopee;
                    break;
                case "wig":
                    holdingGO.GetComponentInChildren<Image>().sprite = wig;
                    break;
                case "soap":
                    holdingGO.GetComponentInChildren<Image>().sprite = soap;
                    break;
                case "balloonanimal":
                    holdingGO.GetComponentInChildren<Image>().sprite = balloonanimal;
                    break;
            }
        }
    }

    void movePlayer()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(player.speed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(player.speed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0f, player.speed * Time.deltaTime, 0f);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0f, player.speed * Time.deltaTime, 0f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collidingObject != null)
        {
            return;
        }
        if(collision.name == "Workbench") {
            if(player.hasItem()) {
                Debug.Log("Player has item");
                Place.SetActive(true);
                collidingObject = collision.gameObject;
            } else if(bench.hasAllItems()) {
                Debug.Log("Bence has allitems" + bench.hasAllItems());
                Perform.SetActive(true);
                collidingObject = collision.gameObject;
            }

        } else {
            Pickup.SetActive(true);
            collidingObject = collision.gameObject;
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Place.SetActive(false);
        Perform.SetActive(false);
        Pickup.SetActive(false);

        collidingObject = null;
    }

}
