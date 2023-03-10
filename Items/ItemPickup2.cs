using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup2 : Interactable
    
{
    public Item item;
    public GameObject firstItem;
    
    public override void Interact()
    {
        base.Interact();

        Pickup();
    }
    void Pickup()
    {
        //Check value of Mining Skill

        //Show progress bar when mining for couple of seconds

        //Effect for the vein to break
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
        {
            
            Destroy(firstItem);
        }
        
    }
}
