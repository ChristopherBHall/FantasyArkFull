using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class TooltipButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private Text text;
    public int fontSizeStart = 18;
    public int fontSizeAdd = 2;
    private int fontSizeNew;
    public bool isInside = false;
    public Action actionMethod;

    private void Start()
    {
        text = GetComponent<Text>();
        fontSizeNew = fontSizeStart + fontSizeAdd;
       
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && isInside == true)
        {
            SetCallAction(actionMethod);
            
        }
    }

    public void SetCallAction(Action action)
    {
        if (action != null)
        {
            action();
        }
        else
        {
            Debug.Log("Error: SetCallAction called unassigned method");
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize = fontSizeNew;
        isInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize = fontSizeStart;
        isInside = false;
    }

}
