using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
public class LocalPlayerController : NetworkBehaviour {

    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        
           
        
    }
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            gameObject.GetComponentInChildren<AudioListener>().enabled = false;
            gameObject.GetComponent<Interact>().enabled = false;
            gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = false;
            gameObject.GetComponent<MeleeSystem>().enabled = false;
            gameObject.GetComponent<Player>().enabled = false;
            gameObject.GetComponent<LocalPlayerController>().enabled = false;
        }
    }
}
