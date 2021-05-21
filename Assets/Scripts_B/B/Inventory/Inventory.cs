using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
   
        AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.FatiguePotion, amount = 1 });
        AddItem(new Item  { itemType = Item.ItemType.Coin, amount = 1 , value =5});

        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    //Expose the inventory items 
    public List<Item> GetItemList()
    {
        return itemList;
    }
}

