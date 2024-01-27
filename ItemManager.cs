using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public List<Item> items;
    // Start is called before the first frame update

    public ItemManager() { 
        items = new List<Item>();
        assignMoodRatings();
        populateListWithInitialValues();

    }

    private void populateListWithInitialValues()
    {
        // Better method would be to get this from a file.
        items.Add(new Item ("rabbit", "rabbitImage", "Rabbit", "A fluffy white rabbit often used for magical tricks. has long ears.", 7 ));
        items.Add(new Item ("banana","bananaImage","Banana Peel", "A slippery fruit skin that can cause someone to slip and fall. A simple but effective gag.", 6 ));
        items.Add(new Item ("flower", "flowerImage", "Flower", "A beautiful plant that can be given as a gift or used to squirt water at someone. A dual-purpose item.", 7 ));
        items.Add(new Item ("rubberduck", "rubberduckImage","Rubber Duck", "A squeaky toy that can be used to make funny noises or surprise someone. A cute and harmless joke.", 9 ));
        items.Add(new Item ("feather", "featherImage","Feather", "A light and fluffy object that can be used to tickle someone or make them sneeze. A gentle and amusing trick.", 6 ));
        items.Add(new Item ("whoopee", "whoopeeImage", "Whoopee Cushion", "A rubber device that makes a farting noise when someone sits on it. A classic prank.", 5 ));
        items.Add(new Item ("fakebeard", "fakebeardImage", "Fake Beard", "A hairy accessory that can be worn to disguise oneself or impersonate someone else. A versatile prop.", 8 ));
        items.Add(new Item ("wig", "wigImage", "Wig", "A fake hairpiece that can be worn to change one's appearance or make fun of someone else. A humorous and creative item.", 8 ));
        items.Add(new Item ("balloonanimal", "balloonanimalImage", "Balloon Animal", "A colorful and inflatable object that can be used to make shapes, pop loudly, or float away. A fun and festive item.", 9 ));
        items.Add(new Item ("soap", "soapImage", "Soap", "A cleaning product that can be used to make bubbles, slip on, or switch with something else. A sneaky and clever item.", 5 ));

        /* dont add for now, since we dont have prefabs.
        items.Add(new Item ("pie", "Pie", "A delicious pastry filled with cream. Can be thrown at someone's face for a laugh.", 7 ));
*/
    }

    public void assignMoodRatings()
    {
        // for the 10 items, we want 5 items that give max positive results 5
        // 3 that given low results 2
        // 2 that given -5 results
        int[] availableOptions = { 3, 3, 3, 3, 3, 0, 0, 0, -5, -5 };

        // for teh 10 items in the list, assign the mood rating from the availableOptions
        foreach (Item item in items)
        {
            int index = Random.Range(0, availableOptions.Length);
            item.moodModifier = availableOptions[index];
            availableOptions[index] = availableOptions[availableOptions.Length - 1];
            System.Array.Resize(ref availableOptions, availableOptions.Length - 1);
        }


    }

    // Get Item
    public Item getItem(string prefabName) {
        // find the item in the list
        foreach (Item item in items)
        {
            if (item.prefab == prefabName)
            {
                return item;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
