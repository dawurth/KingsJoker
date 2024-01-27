using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string prefab;
    public string image;
    public string name;
    public string description;
    public int moodModifier; // do we want to track this here or in a seperate dedicated class.

    public Item(string prefab, string image, string name, string description, int moodModifier)
    {
        this.prefab = prefab;
        this.image = image;
        this.name = name;
        this.description = description;
        this.moodModifier = moodModifier;
    }
}
