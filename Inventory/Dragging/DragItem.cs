using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IDropHandler
{
    public GameObject dropped;
    
    public void OnDrop(PointerEventData eventData)
    {
            
            GameObject dropped = eventData.pointerDrag; // Which object you dropped into slot.
            DragHandler draggableItem = dropped.GetComponent<DragHandler>();
        
        //Debug.Log(draggableItem.parentAfterDrag); //From which Inventory the item leaves.
        //Debug.Log(draggableItem.parentAfterDrag.GetChild(0));
        
        draggableItem.parentAfterDrag = transform; //To which Inventory slot

        
        draggableItem.parentAfterDragSecond = transform.GetChild(0); //Get the first child (Button)

        
        Debug.Log(draggableItem.parentAfterDrag = transform);
       

        //Debug.Log(draggableItem.parentAfterDragSecond = transform.GetChild(0));
        
        //Debug.Log(draggableItem.parentAfterDrag = transform); //Destination target
        //Debug.Log(draggableItem.parentAfterDrag.GetChild(0)); //Destination target's first child
    }
    
}
