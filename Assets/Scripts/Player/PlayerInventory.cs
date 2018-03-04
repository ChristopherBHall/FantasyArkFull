using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
[SerializeField]
public class PlayerInventory : NetworkBehaviour {
    //public int slotAmount = 100;
   // public GameObject slotPrefab;
    //public GameObject slotParent;
    //public GameObject toolbar;
    //public GameObject itemPrefab;

   // public GameObject inventoryWindow;
    
   // public GameObject contentCanvas;

    private ItemDatabase itemDatabase;
    //private PauseManager pauseManager;

    public string userName;
    public List<Item> playerInventory = new List<Item>();


    public KeyCode inventoryKey = KeyCode.I;
    public bool isShown = false;

    public float defaultXSens;
    public float defaultYSens;
    // Use this for initialization
    [SerializeField]
    public override void OnStartLocalPlayer()
    {
        if (isLocalPlayer) { 
            if (itemDatabase == null)
            {
                itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
            }

            playerInventory.Add(itemDatabase.GetItemByID(0));
            playerInventory.Add(itemDatabase.GetItemByID(1));
        }
    }

}
