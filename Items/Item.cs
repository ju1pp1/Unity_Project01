using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int ID;
    //Testing
    public bool isStackable = false;
    public int maxStacks = 10;
    [NonSerialized]
    public int itemAmount = 0; //readonly
    [NonSerialized]
    public bool getThisBoolOnlyOnce = true;
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
    /*
    public void NewItem(Item newItem)
    {
        this.name = newItem.name;
        this.icon = newItem.icon;
        this.isDefaultItem = newItem.isDefaultItem;
        this.ID = newItem.ID;
        this.isStackable = newItem.isStackable;
        this.maxStacks = newItem.maxStacks;
        this.itemAmount = newItem.itemAmount;
    }*/
}
