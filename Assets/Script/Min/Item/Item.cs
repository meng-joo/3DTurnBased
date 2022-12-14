using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int item_id = -1;
    public string item_name;

    public Item()
    {
        item_id = -1;
        item_name = "";
    }

    public ItemAbility[] abilities;

    public Item(ItemObj itemObj)
    {
        item_name = itemObj.name;
        item_id = itemObj.itemData.item_id;
        abilities = new ItemAbility[itemObj.itemData.abilities.Length];

        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i] = new ItemAbility(itemObj.itemData.abilities[i].Min, itemObj.itemData.abilities[i].Max)
            {
                characterStack = itemObj.itemData.abilities[i].characterStack
            };
        }
    }
}
