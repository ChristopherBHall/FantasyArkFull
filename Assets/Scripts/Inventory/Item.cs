using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item {

    public string itemName;
    public string itemDesc;
    public Sprite itemSprite;
    public string itemSpriteName;
    public int itemID;
    public string itemType;
    public int itemFood;
    public int itemWater;
    public int itemHealth;
    public float itemWeight;
    public bool itemBreakable;
    public int itemDurability;
    public bool itemStackable;
    public int itemStackLimit;


    public Item(string name, string desc, string spriteFileName, string spriteName, int id, string type, int health, int food, int water, float weight, bool breakable, int durability, bool stack, int stackLimit)
    {
        itemName = name;
        itemDesc = desc;
        itemSprite = Resources.Load<Sprite>("ItemIcons/" + spriteFileName);
        itemSpriteName = spriteName;
        itemID = id;
        itemType = type;
        itemHealth = health;
        itemFood = food;
        itemWater = water;
        itemWeight = weight;
        itemBreakable = breakable;
        itemDurability = durability;
        itemStackable = stack;
        itemStackLimit = stackLimit;
        
    }
    public Item()
    {

    }
}
