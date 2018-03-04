using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
public class ItemObj : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler  {

    private Transform startParent;
    private Player player;

    private CanvasGroup canvasGroup;

    public GameObject RightClickTooltip;
    public Vector2 offSet;

    //private bool inItem = false;
    private ItemDatabase itemDB;

    public GameObject MouseOverTooltip;
    public Text MouseOverName;
    public Text MouseOverDesc;
    public Text MouseOverType;

    public TooltipButton tooltipButton1;
    public TooltipButton tooltipButton2;
    public int currentItemID;
    public GameObject currentSelectedSlot;
    public int stackAmount;

    public bool isCountedSlot;
    public int slotNum;


    private void onStartLocalPlayer()
    {
        startParent = transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
        itemDB = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        MouseOverName = GameObject.FindGameObjectWithTag("MouseOverName").GetComponent<Text>();
        MouseOverDesc = GameObject.FindGameObjectWithTag("MouseOverDesc").GetComponent<Text>();
        MouseOverType = GameObject.FindGameObjectWithTag("MouseOverType").GetComponent<Text>();
        MouseOverTooltip = GameObject.FindGameObjectWithTag("MouseOverTooltip");
        RightClickTooltip = GameObject.FindGameObjectWithTag("RightClickTooltip");
        tooltipButton1 = GameObject.FindGameObjectWithTag("TooltipButton1").GetComponent<TooltipButton>();
        tooltipButton2 = GameObject.FindGameObjectWithTag("TooltipButton2").GetComponent<TooltipButton>();
        offSet = new Vector2(450, -200);


        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       

    }

    private void Update()
    {

    }
    private void OnMouseOver()
    {
        Debug.Log("Test");
    }


    private void DebugTest()
    {
        Debug.Log("We passed methods and used our tooltip");
    }
    private void UseItem()
    {
        switch (currentSelectedSlot.GetComponent<ItemObj>().MouseOverType.text)
        {
            case "Consumable":
                player.AddHealth(itemDB.GetItemByID(currentSelectedSlot.GetComponent<ItemObj>().currentItemID).itemHealth);
                player.AddHunger(itemDB.GetItemByID(currentSelectedSlot.GetComponent<ItemObj>().currentItemID).itemFood);
                player.AddThirst(itemDB.GetItemByID(currentSelectedSlot.GetComponent<ItemObj>().currentItemID).itemWater);
                Destroy(gameObject);
                Debug.Log("Consumable");
                break;
            case "Crafting":
                Debug.Log("Crafting");
                break;
            case "Tool":
                Debug.Log("Tool");
                break;
            default:
                break;
        }
    }
    private void GetItemChild()
    {

    }
    public void SplitInventoryStack()
    {

        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");

        foreach (var slot in slots)
        {
            if (slot.GetComponent<Slot>().isSelected == true)
            {
                //check is slot is not empty
                if (slot.transform.childCount >= 1)
                {
                    startParent = slot.transform;
                    //Getting ItemObj from existing item slot
                    ItemObj otherItem = slot.transform.GetChild(0).GetComponent<ItemObj>();


                    //storing other object parent
                    Transform otherItemParent = otherItem.startParent;

                    //moving other item to our old parent position
                    otherItem.startParent = startParent;

                    //Set parent to new slot
                    startParent = otherItemParent;

                    otherItem.transform.SetParent(otherItem.startParent);
                    otherItem.transform.localPosition = Vector2.zero;
                    otherItem.stackAmount = otherItem.stackAmount / 2;
                    GameObject itemInstance = Instantiate(transform.gameObject);
                    itemInstance.transform.SetParent(startParent);
                    itemInstance.transform.localPosition = Vector2.zero;
                    //itemInstance.GetComponent<Image>().sprite = itemAdd.itemSprite; // set sprite
                    //itemInstance.GetComponent<ItemObj>().currentItemID = id;
                    //itemsInInventory.Add(id);
                    //contentCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(contentCanvas.GetComponent<RectTransform>().sizeDelta.x, 80 + (80 * (itemsInInventory.Count / 4)));
                    //itemInstance.GetComponent<ItemObj>().stackAmount = 1;
                }
                else
                {
                    startParent = slot.transform;
                }

                break;

            }
        }
        transform.SetParent(startParent);
        transform.localPosition = Vector2.zero;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GetItemChild();
        }
         else{
            GetItemChild();
        } 
        


        transform.position = eventData.position;

        transform.SetParent(transform.parent.parent);

        //disable block raycast to fix highlight / mouseover issue
        canvasGroup.blocksRaycasts = false;
    }


    public void OnDrag(PointerEventData eventData)
    {

        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //SplitInventoryStack();
            GetSlotInfo();
        }
        else
        {
            GetSlotInfo();
        }
        
        canvasGroup.blocksRaycasts = true;


    }
    private void GetSlotInfo()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");

        foreach(var slot in slots)
        {
            if(slot.GetComponent<Slot>().isSelected == true)
            {
                //check is slot is not empty
                if(slot.transform.childCount >= 1)
                {
                    //Getting ItemObj from existing item slot
                    ItemObj otherItem = slot.transform.GetChild(0).GetComponent<ItemObj>();
                    

                    //storing other object parent
                    Transform otherItemParent = otherItem.startParent;

                    //moving other item to our old parent position
                    otherItem.startParent = startParent;

                    //Set parent to new slot
                    startParent = otherItemParent;

                    otherItem.transform.SetParent(otherItem.startParent);
                    otherItem.transform.localPosition = Vector2.zero;
                    
                }
                else
                {
                    startParent = slot.transform;
                }
                
                break;

            }
        }
        transform.SetParent(startParent);
        transform.localPosition = Vector2.zero;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        //inItem = true;
        MouseOverName.text = itemDB.GetItemByID(currentItemID).itemName;
        MouseOverDesc.text = itemDB.GetItemByID(currentItemID).itemDesc;
        MouseOverType.text = itemDB.GetItemByID(currentItemID).itemType;
        MouseOverTooltip.transform.localPosition = new Vector3(450, -200, 0);






    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //inItem = false;
        MouseOverTooltip.transform.localPosition = new Vector3(4500, -2000, 0);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject pressed = eventData.pointerPress;

        //Right click
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (eventData.clickCount == 1)
            {
                currentSelectedSlot = pressed;
                RightClickTooltip.GetComponent<Tooltip>().ToggleTooltip(true);
                tooltipButton1.actionMethod = UseItem;
                tooltipButton2.actionMethod = DebugTest;
            }
            else if (eventData.clickCount == 2)
            {
                //Keeping this here to remind myself I can double click here
            }
        }
    }
}
