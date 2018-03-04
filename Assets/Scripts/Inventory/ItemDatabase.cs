using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

    private string itemTypeCraft = "Crafting";
    private string itemTypeTool = "Tool";
    private string itemTypeConsumable = "Consumable";

    public List<Item> items = new List<Item>();

    private void Start()
    {
        ItemDataBaseSetup();
    }
    private void ItemDataBaseSetup()
    {
        // Name , Description, Sprite File Name, Sprite Name, ID, itemType, food, water, weight, breakable, durability, stackable, stackAmount
        items.Add(new Item("Wood", "A piece of wood", "Wood", "Wood", 0, itemTypeCraft, 0, 0, 0, 0.5f , false, 100, true, 99));
        items.Add(new Item("Stone", "A piece of stone", "Stone", "Stone" , 1, itemTypeCraft, 0, 0, 0, 0.5f, false, 100, true, 99));
        items.Add(new Item("Axe", "Basic hand axe", "Axe", "Axe", 2, itemTypeTool, 0, 0, 0, 2, true, 100, false, 0));
        items.Add(new Item("Pickaxe", "Basic pick axe", "Pickaxe", "Pickaxe", 3, itemTypeTool, 0, 0, 0, 2, true, 100, false, 0));
        items.Add(new Item("Meat", "Basic piece of meat", "Meat", "Meat", 4, itemTypeConsumable, 0, 25, 0, 0.2f, true, 100, true, 20));
        items.Add(new Item("Water", "Basic piece of water, wait what?", "Water", "Water", 5, itemTypeConsumable, 0, 0, 15, 1, false, 100, true, 20));
    }
    public Item GetItemByID(int id)
    {
        foreach(Item itm in items)
        {
            if(itm.itemID == id)
            {
                return itm;
            }
        }
        Debug.LogError("Can't find item by ID!");
        return null;
    }
}
