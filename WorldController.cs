using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WorldController : MonoBehaviour
{
    ItemManager itemManager;
    List<GameObject> spawnedItems;

    // Ideally we get this externally but for now ill just do this.
    public GameObject rabbit;
    public GameObject banana;
    public GameObject flower;
    public GameObject rubberduck;
    public GameObject feather;
    public GameObject wig;
    public GameObject balloonanimal;
    public GameObject whoopee;
    public GameObject fakebeard;
    public GameObject soap;

    public GameObject displayTimer;

    // Start is called before the first frame update
    void Start()
    {
        
        itemManager = new ItemManager();
        itemManager.assignMoodRatings();
        spawnItems();

    }

    public void spawnItems()
    {
        //TODO make sure the randomize doesn't overlap
        // Before spawning, clear all the exisiting children so we dont have overlaps and everything has a chance of being in a new position
        clearItems();
        // Y between 4 -4
        // X between 8 -8
        // should not overlap with the table.
        // y skip 1 and 2 if x is between -2 to 2
        // x skip -2 to 2 if y is 1 or 2.
        //foreach item in itemManager spawn a prefab
        //ideally no overlaps but low priority.
        foreach (Item item in itemManager.items)
        {
            switch(item.prefab) {
                case "rabbit":
                    createItem(rabbit, item.prefab, generateSpawnCoords());
                    break;
                case "banana":
                    createItem(banana, item.prefab, generateSpawnCoords());
                    break;
                case "flower":
                    createItem(flower, item.prefab, generateSpawnCoords());
                    break;
                case "rubberduck":
                    createItem(rubberduck, item.prefab, generateSpawnCoords());
                    break;
                case "feather":
                    createItem(feather, item.prefab, generateSpawnCoords());
                    break;
                case "fakebeard":
                    createItem(fakebeard, item.prefab, generateSpawnCoords());
                    break;
                case "whoopee":
                    createItem(whoopee, item.prefab, generateSpawnCoords());
                    break;
                case "wig":
                    createItem(wig, item.prefab, generateSpawnCoords());
                    break;
                case "soap":
                    createItem(soap, item.prefab, generateSpawnCoords());
                    break;
                case "balloonanimal":
                    createItem(balloonanimal, item.prefab, generateSpawnCoords());
                    break;
            }
        }
    }

    private Vector3 generateSpawnCoords()
    {

        // Generate a random y coordinate between -4 and 4
        int y = Random.Range(-4, 5);

        // Declare a variable for the x coordinate
        int x;

        // If y is 1 or 2, then x can't be between -2 and 2
        if (y == 1)
        {
            // Generate a random x coordinate between -8 and -3 or between 3 and 8
            x = Random.Range(-8, 3);
            
        }
        else if (y == 2)
        {
            // Generate a random x coordinate between -8 and 8
            x = Random.Range(3, 8);
        } else
        {
            x = Random.Range(-8, 8);
        }

        return new Vector3 (x, y, 0);
    }

    void createItem(GameObject obj, string name, Vector3 coords)
    {
        GameObject itemGo = Instantiate(obj, coords, Quaternion.identity);
        itemGo.transform.SetParent(this.transform, false);
        itemGo.name = name;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void clearItems()
    {
        // remove all children of this compondent.
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

    }
}
