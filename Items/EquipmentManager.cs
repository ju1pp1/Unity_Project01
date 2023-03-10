using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton
    public static EquipmentManager instance;

    void Awake ()
    {
        instance = this;
    }
    #endregion

    public Equipment[] defaultItems; //currentEquipment //defaultItems
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment; // Items we currently have equipped.
    SkinnedMeshRenderer[] currentMeshes; //Keep track of meshes spawned into the scene.

    //Callback for when an item is equipped/unequipped.
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory; //Reference to our inventory.

    void Start ()
    {
        inventory = Inventory.instance; //Get a reference to our inventory.
        
        //Initialize currentEquipment based on number of equipment slots.
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    //Equip a new item.
    public void Equip (Equipment newItem)
    {
        //Find out what slot the item fits in.
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = null;

        //If there was already an item in the slot
        //make sure to put it back in the inventory
        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            
        }

        //An item has been equipped so we trigger the callback.
        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        
        SetEquipmentBlendShapes(newItem, 100);
        
        //Insert the item into the slot.
        currentEquipment[slotIndex] = newItem;

        //Instantiate new equipment mesh
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform; //Set its parent object to target, refers to player mesh.

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        //insert our new mesh to current mesh.
        currentMeshes[slotIndex] = newMesh;

        
    }

    public void Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            if(onEquipmentChanged != null)
            {

            }
        }
    }
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }
    
    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }
     //Unequipment by dragging item with cursor (Jaksot 7 ja 8 Brackeys)
}
