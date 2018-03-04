using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Interact : NetworkBehaviour {
    public float interactRage = 3f;
    public Camera FPSCamera;

    private FoodItem foodItem;
    private Player player;
    private Inventory inventory;
    public GameObject IsThisEvenWorking;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

    }



    // Update is called once per frame
    [Command]
    void Cmd_InteractWithObject(GameObject hitGameObject)
    {

        if (hitGameObject.tag == "FoodItem")
        {
            foodItem = hitGameObject.GetComponent<FoodItem>();

            if (foodItem.hungerType == FoodItem.HungerType.Food)
            {
                inventory.AddItem(4, 1);
                Destroy(hitGameObject);

            }
            else if (foodItem.hungerType == FoodItem.HungerType.Water)
            {
                inventory.AddItem(5, 1);
                Destroy(hitGameObject);
            }
        }
        else if (hitGameObject.tag == "Water")
        {
            if (foodItem.hungerType == FoodItem.HungerType.Water)
            {
                player.AddThirst(player.thirstMax);
            }
        }





        //NetworkIdentity objNetID = hit.GetComponent<NetworkIdentity>();
        //Destroy(objNetID.gameObject);
        

    }
    void Update () {
        if (isLocalPlayer)
        {
            Ray ray = FPSCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitInfo;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(ray, out hitInfo, interactRage))
                {
                    Cmd_InteractWithObject(hitInfo.collider.gameObject);
                }
            }
        }
	}
}
