using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler {

    public bool isSelected = false;
    private Outline outline;

    private Transform startParent;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;  
    }
    private void OnDisable()
    {
        if (outline)
        {
            outline.enabled = false;
            isSelected = false;
        }
    }






public void OnPointerEnter(PointerEventData eventData)
    {
        outline.enabled = true;
        isSelected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
        isSelected = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        outline.enabled = false;
        isSelected = false;
    }
}
