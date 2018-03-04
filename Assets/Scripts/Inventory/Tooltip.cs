using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    public Vector2 offSet;


    public GameObject RightClickTooltip;
    public Image backgroundImage;
    public Text button1;
    public Text button2;

    private void Start()
    {
        //ToggleTooltip(false, Vector2.zero);
        RightClickTooltip = GameObject.FindGameObjectWithTag("RightClickTooltip");
        RightClickTooltip.transform.position = new Vector3(4000, 4000, 0);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ToggleTooltip(false);
            RightClickTooltip.transform.position = new Vector3(4000, 4000, 0);

        }
            
    }
    public void ToggleTooltip(bool enabled, Vector2 position)
    {
        if(enabled == true)
        {
           // tooltipObject.SetActive(true);
            //backgroundImage.enabled = true;
           // button1.enabled = true;
           // button2.enabled = true;
        }
        else if (enabled == false)
        {
            //tooltipObject.SetActive(false);
           // backgroundImage.enabled = false;
          //  button1.enabled = false;
           // button2.enabled = false;
        }

       // transform.position = position + offSet;
    }
    public void ToggleTooltip(bool enabled)
    {
        if (enabled == true)
        {
            RightClickTooltip.transform.position = Input.mousePosition;
            // backgroundImage.enabled = true;
            // button1.enabled = true;
            // button2.enabled = true;
        }
        else if (enabled == false)
        {
            RightClickTooltip.transform.position = new Vector3(4000, 4000, 0);
           //backgroundImage.enabled = false;
          //  button1.enabled = false;
          //  button2.enabled = false;
        }

        RightClickTooltip.transform.position = Input.mousePosition + new Vector3(offSet.x, offSet.y, 0);
    }
}
