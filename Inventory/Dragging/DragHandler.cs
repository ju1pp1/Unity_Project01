using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //, IPointerDownHandler
{
    public Image image;
    public Transform parentAfterDrag; //[HideInInspector]
    public Transform parentAfterDragSecond;
    public DragItem dragItem;
    InventorySlot inventorySlot;
    public Transform temp; //Inventory slot the item leaves
    public void OnBeginDrag(PointerEventData eventData)
    {
        temp = transform.parent;

        image.raycastTarget = false;
        //Debug.Log("Begin drag");
        
        parentAfterDrag = transform.parent; //Parent is inventory slot

        inventorySlot = parentAfterDrag.gameObject.GetComponent<InventorySlot>();

        //Debug.Log(parentAfterDrag);
        //Debug.Log(parentAfterDrag.gameObject);
        parentAfterDragSecond = transform.parent.GetChild(0); //Gets the first child
        
        transform.SetParent(transform.root); //root = Canvas 
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging...");

        transform.position = Input.mousePosition;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        image.raycastTarget = true;
        //    Debug.Log("End drag");

        //parentAfterDragSecond.SetParent(parentAfterDrag);
        //Debug.Log(parentAfterDragSecond);
        //Debug.Log(temp);
        //Debug.Log(inventorySlot.item);

        transform.SetParent(parentAfterDrag);
        parentAfterDragSecond.SetParent(temp);
        parentAfterDragSecond.SetAsFirstSibling();
        transform.SetAsFirstSibling();
        
        
        //Debug.Log(transform.parent.gameObject);


    }
    /*
    public void OnPointerDown(PointerEventData eventData)
    {
        parentAfterDrag.SetAsFirstSibling();
    }
    */
}
