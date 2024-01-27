using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Workbench
{
    private List<Item> items;
    public int MAX_ITEMS = 5;

    public Workbench()
    {
        items = new List<Item>();
    }

    public void addItem(Item item)
    {
        if (items.Count <= MAX_ITEMS)
        {
            Debug.Log("Adding item:" + item.name + " current count: " + items.Count);
            items.Add(item);
        }
    }

    public bool hasAllItems()
    {
        if(items.Count == MAX_ITEMS)
        {
            Debug.Log("Items.Count = MAX ITEMS" + items.Count);
            return true;
        }
        return false;
    }

    public int getItemCount()
    {
        return items.Count;
    }

    public void clearItems()
    {
        items.Clear();
    }

    internal int calculateMood()
    {
        int mood = 0;
        foreach (Item item in items)
        {
            mood += item.moodModifier;
        }
        return mood;
    }
}
