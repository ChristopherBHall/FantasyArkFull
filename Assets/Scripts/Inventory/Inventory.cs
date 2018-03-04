using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {

    public int slotAmount = 100;
    public GameObject slotPrefab;
    public GameObject slotParent;
    public GameObject toolbar;
    public GameObject itemPrefab;
    
    public GameObject inventoryWindow;
    public RigidbodyFirstPersonController fpsController;
    public GameObject contentCanvas;

    public ItemDatabase itemDatabase;
    public PauseManager pauseManager;


    private List<GameObject> slots = new List<GameObject>();
    public List<int> itemsInInventory = new List<int>();
    public List<GameObject> inventoryResize = new List<GameObject>();


    public KeyCode inventoryKey = KeyCode.I;
    public bool isShown = false;

    public float defaultXSens;
    public float defaultYSens;




    // Use this for initialization
    public void Start() {
        slotAmount = 100;
        itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        pauseManager = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<PauseManager>();
        //fpsController = GameObject.FindGameObjectWithTag("Player").GetComponent<RigidbodyFirstPersonController>();
        toolbar = GameObject.FindGameObjectWithTag("Toolbar");
        defaultXSens = fpsController.mouseLook.XSensitivity;
        defaultYSens = fpsController.mouseLook.YSensitivity;
        // inventoryCanvas.SetActive(false);
        CreateSlots(slotAmount);
        CreateToolbar();
        inventoryWindow.SetActive(false);

    }
    [Client]
    private void Update()
    {


            if (Input.GetKeyDown(inventoryKey) && pauseManager.isPaused == false)
            {

                isShown = !isShown;
                if (isShown == true)
                {
                    //inventoryWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(20 * slotAmount, 20 * slotAmount);
                    inventoryWindow.SetActive(true);
                    fpsController.mouseLook.SetCursorLock(false);
                    fpsController.mouseLook.XSensitivity = 0.0f;
                    fpsController.mouseLook.YSensitivity = 0.0f;








                }
                else if (isShown == false)
                {
                    fpsController.mouseLook.SetCursorLock(true);
                    inventoryWindow.SetActive(false);
                    fpsController.mouseLook.XSensitivity = defaultXSens;
                    fpsController.mouseLook.YSensitivity = defaultYSens;

                }
                
            }

    }

    /*private void ResizeInventory(int slotsAmount)
    {
        for (int i = 0; i < slotsAmount; i++)
        {
            if (slots[i].transform.childCount >= 1 && slots[i].transform.parent.tag == "SlotsParent")
            {

                if (slots[i].GetComponentInChildren<ItemObj>())
                {
                    if (slots[i].GetComponentInChildren<ItemObj>().isCountedSlot == false)
                    {
                       
                        slots[i].GetComponentInChildren<ItemObj>().isCountedSlot = true;
                        inventoryResize.Add(slots[i]);
                        
                    }
                }
                
            }
        }
        contentCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(contentCanvas.GetComponent<RectTransform>().sizeDelta.x, 135 * ((inventoryResize.Count / 4)));
    }
    */

	
    private void CreateSlots(int slotsAmount)
    {
        for(int i = 0; i < slotsAmount; i++)
        {
            slots.Add(Instantiate(slotPrefab));
            slots[i].transform.SetParent(slotParent.transform);
            
        }

    }
    private void CreateToolbar()
    {
        for (int a = 0; a < 10; a++)
        {
            slots[a].transform.SetParent(toolbar.transform);

        }
    }

    public void AddItem(int id, int amount)
    {
        if(itemDatabase == null)
        {
            itemDatabase = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        }
        Item itemAdd = itemDatabase.GetItemByID(id);
        for(int i = 10; i < slots.Count; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                if (slots[i].GetComponentInChildren<ItemObj>())
                {
                    if(slots[i].GetComponentInChildren<ItemObj>().currentItemID == id)
                    {
                        if (slots[i].GetComponentInChildren<ItemObj>().stackAmount < itemDatabase.GetItemByID(id).itemStackLimit)
                        {
                            slots[i].GetComponentInChildren<ItemObj>().stackAmount += amount;
                            slots[i].GetComponentInChildren<Text>().text = slots[i].GetComponentInChildren<ItemObj>().stackAmount.ToString();
                            break;
                        }
                        else
                        {
                            for (int a = 0; a < slots.Count; a++)
                            {
                                if (slots[i].transform.childCount > 0)
                                {
                                }
                                else
                                {
                                    NewSlottedItem(id, a, itemAdd, amount);
                                    break;

                                }
                            }
                        
                        }
                    }
                }
                else
                {
                    Debug.Log("ItemObj apparently doesn't exist in AddItem... How?");
                    break;
                }

                           

            }
            else if(slots[i].transform.childCount <= 0)
            {
                NewSlottedItem(id, i, itemAdd, amount);
                break;
            }


        }
        
    }
    public void NewSlottedItem(int id, int i, Item itemAdd, int amount)
    {
        GameObject itemInstance = Instantiate(itemPrefab); // Create item object
        itemInstance.transform.SetParent(slots[i].transform);
        itemInstance.transform.localPosition = Vector2.zero;
        itemInstance.GetComponent<Image>().sprite = itemAdd.itemSprite; // set sprite
        itemInstance.GetComponent<ItemObj>().currentItemID = id;
        itemsInInventory.Add(id);
        
        itemInstance.GetComponent<ItemObj>().stackAmount += amount;
    }
}



