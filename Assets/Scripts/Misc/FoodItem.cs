using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FoodItem : NetworkBehaviour {

    public enum HungerType {Food, Water };
    public HungerType hungerType = new HungerType();



    void Update()
    {
       

    }

}
