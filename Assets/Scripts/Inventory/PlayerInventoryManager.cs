using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class PlayerInventoryManager : NetworkBehaviour {

    private ItemDatabase itemDatabase;
    //private PauseManager pauseManager;

    public string userName;





    // Use this for initialization
    [SerializeField]
    public override void OnStartLocalPlayer()
    {
        if (isLocalPlayer)
        {
            if (itemDatabase == null)
            {
                itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
            }


            playerInventoryList.list.Add(new PlayerInventoryClass(itemDatabase.GetItemByID(0).itemName, itemDatabase.GetItemByID(0).itemID, 1, 100));
        }
    }
    void OnPublicConnected(NetworkPlayer player)
    {
        Cmd_LoadPlayerInfo();
        playerInventoryList.list.Add(new PlayerInventoryClass(itemDatabase.GetItemByID(0).itemName, itemDatabase.GetItemByID(0).itemID, 1, 100));
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        if (Network.isServer) { 
             Cmd_SavePlayerInfo();
            Debug.Log("Local server connection disconnected");
        }
        else
        {
            if (info == NetworkDisconnection.LostConnection)
            {
                Cmd_SavePlayerInfo();
            }
            else
            {
                Cmd_SavePlayerInfo();
            }
        }
    }
public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Cmd_SavePlayerInfo();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Cmd_LoadPlayerInfo();
        }
    }

    public static PlayerInventoryManager ins;

    private void Awake()
    {
        ins = this;
    }

    //list of inventory
    [XmlArray("PlayerInventory")]
    public PlayerInventoryList playerInventoryList;
    //save funtion
    [Command]
    public void Cmd_SavePlayerInfo()
    {
        Debug.Log("Save");
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerInventoryList));
        //FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/PlayerSaves/"+ SteamUser.GetSteamID() + ".xml", FileMode.Create);
       // serializer.Serialize(stream, playerInventoryList);
       // stream.Close();
    }
    [Command]
    public void Cmd_LoadPlayerInfo()
    {
        Debug.Log("Load");
       // if (File.Exists(Application.dataPath + "/StreamingAssets/PlayerSaves/" + SteamUser.GetSteamID() + ".xml")) {   }
        //else { FileStream streamCreate = new FileStream(Application.dataPath + "/StreamingAssets/PlayerSaves/" + SteamUser.GetSteamID() + ".xml", FileMode.Create); }
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerInventoryList));
        //stream.Close();
    }
}
[Serializable]
public class PlayerInventoryClass
{
    public string itemName;
    public int itemID;
    public int itemAmount;
    public float itemDurability;

    public PlayerInventoryClass(string name, int id, int amount, float durability)
    {
        itemName = name;
        itemID = id;
        itemAmount = amount;
        itemDurability = durability;
    }

}
[Serializable]
public class PlayerStatsClass
{
    public string playerName;
    public int playerID;
    public int playerHealth;
    public int playerMaxHealth;
    public int playerThirst;
    public int playerThirstMax;
    public int playerHunger;
    public int playerHungerMax;

}
[Serializable]
public class PlayerInventoryList
{
    public List<PlayerInventoryClass> list = new List<PlayerInventoryClass>();
    public List<PlayerStatsClass> statList = new List<PlayerStatsClass>();
}

[Serializable]
public class PlayerStatsList
{
    public List<PlayerStatsClass> list = new List<PlayerStatsClass>();
}
public enum ItemType
{
    itemTypeCraft,
    itemTypeTool,
    itemTypeConsumable
}