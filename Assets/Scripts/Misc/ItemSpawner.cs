using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemSpawner : NetworkBehaviour {
    public GameObject water;
    public bool spawnPlz;
    // Use this for initialization
    public void OnConnectedToServer() {
        Network.Instantiate(water, GameObject.FindGameObjectsWithTag("FoodSpawn")[0].transform.position, GameObject.FindGameObjectsWithTag("FoodSpawn")[0].transform.rotation, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if(spawnPlz == true)
        {
            GameObject WaterObj = Instantiate(water, GameObject.FindGameObjectsWithTag("FoodSpawn")[0].transform.position, GameObject.FindGameObjectsWithTag("FoodSpawn")[0].transform.rotation);
            NetworkServer.Spawn(WaterObj);
            spawnPlz = false;
        }
    }
}
