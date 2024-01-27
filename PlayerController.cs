using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

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
    public GameObject WorkBenchGO;
    public GameObject LaughBarGO;


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
        itemManager = new ItemManager();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();

        // set player object position to same as player position
        if(this.transform.position.x != player.x || this.transform.position.y != player.y) {
            this.transform.position = new Vector3(player.x, player.y, 0);
            //camera position follows the player
            cam.transform.position = new Vector3(player.x, player.y, -10);
            // call function setMood in LaughBarController
            
        }

        //if the player presses e, if there is a colliding object it should be picked up.
        getItem();
        // If the player presses e at a workbench while using the item it should be placed on the workbench
        useItem();
        // if the player is at a workbench with 5 items and presses p the player should perform
        perform();
    }

    void perform()
    {
        if (Input.GetKeyDown(KeyCode.P) || collidingObject != null && collidingObject.name == "Workbench" && bench.hasAllItems())
        {
            Debug.Log("Performing");
            // Id love if I could get all 5 items to circle around the player and then disappear
            // TODO All items circle the player
            // based on the items selected and if the king likes them, a final total will be added to the bar, once it hits 100 you win, if it hits 0 you die
            int moodResult = bench.calculateMood();
            LaughBarGO.GetComponent<LaughBarController>().setMood(moodResult);

            bench.clearItems();
            // clear all items from the WorkBenchGo as well
            GameObject placedItem = WorkBenchGO.transform.Find("Item1").gameObject;
            placedItem.GetComponent<SpriteRenderer>().enabled = false;

            placedItem = WorkBenchGO.transform.Find("Item2").gameObject;
            placedItem.GetComponent<SpriteRenderer>().enabled = false;

            placedItem = WorkBenchGO.transform.Find("Item3").gameObject;
            placedItem.GetComponent<SpriteRenderer>().enabled = false;

            placedItem = WorkBenchGO.transform.Find("Item4").gameObject;
            placedItem.GetComponent<SpriteRenderer>().enabled = false;

            placedItem = WorkBenchGO.transform.Find("Item5").gameObject;
            placedItem.GetComponent<SpriteRenderer>().enabled = false;
            // after the performance we will need to respawn all the items.
            // how do we let the world controller know to respawn
            GameObject worldController = GameObject.Find("WorldController");
            worldController.GetComponent<WorldController>().spawnItems();

           
        }
    }
    // If the player is able to interact with a bench the item can be used at, if the key is pressed remove the item from the player, and add to the workbench
    // if the workbench has all items then the player will get an option to preform
    void useItem()
    {
        if (Input.GetKeyDown(KeyCode.R) && collidingObject != null && player.hasItem() && collidingObject.name == "Workbench")
        {   
            Item heldItem = player.getItem();
            bench.addItem(heldItem);
            player.removeHeldItem();
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
        }
    }

    void movePlayer()
    {
        // move player object based on arrow keys
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            player.y += player.speed;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            player.y -= player.speed;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            player.x -= player.speed;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            player.x += player.speed;
        }
        // set a player min/max x-y and dont let the values go beyond them
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Workbench") {
            if(player.hasItem()) {
                Debug.Log("Player has item");
                Place.SetActive(true);
            } else if(bench.hasAllItems()) {
                Debug.Log("Bence has allitems" + bench.hasAllItems());
                Perform.SetActive(true);
            }

        } else {
            Pickup.SetActive(true);
        }

        collidingObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Place.SetActive(false);
        Perform.SetActive(false);
        Pickup.SetActive(false);

        collidingObject = null;
    }

}
