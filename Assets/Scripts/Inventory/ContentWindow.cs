using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWindow : MonoBehaviour {

    public GameObject contentWindow;

	// Use this for initialization
	void Start () {
        contentWindow = GameObject.FindGameObjectWithTag("Content");
        contentWindow.GetComponent<RectTransform>().position = new Vector3(contentWindow.GetComponent<RectTransform>().position.x, 0, contentWindow.GetComponent<RectTransform>().position.z); //Y POS kept changing to 80 seemingly randomly which makes the inventory opening look kinda off.. not 100% sure how better to control this, so just forcing it to 0 on start fixes the "bug"
	}
	

}
