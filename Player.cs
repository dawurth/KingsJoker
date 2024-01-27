using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Player
{
    // the players current position
    public float x, y;
    public float speed = 0.10f;

    // Player can only hold a single item
    Item item;

    public void addItem(Item item)
    {
        this.item = item;
        Debug.Log("Player picked up " + item.name);
    }

    public Item getItem()
    {
        return this.item;
    }

    public bool hasItem()
    {
        if (this.item != null)
        {
            return true;
        }
        return false;
    }

    public void removeHeldItem()
    {
        if (this.item != null)
        {
            this.item = null;
        }
    }
}
