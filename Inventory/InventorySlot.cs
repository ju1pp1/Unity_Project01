
using System;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Item item;

    public Text countText;
    [NonSerialized]
    public int counter = 0;
    public int counter2 = 0;
    public int index;
    Inventory inventory;
    public Transform gameManager;
    
    public Transform itemsParent;
    public Transform canvas;
    InventorySlot inventorySlot;
    //public Item storeItem;
    InventorySlot[] slots;
    InventoryUI inventoryUI;
    public Item itemItem;
    //public bool IsEmpty => item == null;

    void Start()
    {
        countText.gameObject.SetActive(false);
        inventorySlot = itemsParent.GetComponentInChildren<InventorySlot>();
        inventoryUI = canvas.GetComponent<InventoryUI>();

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventory = gameManager.GetComponent<Inventory>();

        

    }


    public void AddItem(Item newItem)
    //public bool AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
        
        countText.gameObject.SetActive(true);
        countText.text = item.itemAmount.ToString();

        if (item.isStackable == false)
        {
            bool textActive = item.itemAmount > 1; //Don't show amount if just 1.
            countText.gameObject.SetActive(textActive);
            //return true;
        }

        if (item.isStackable == true)
        {
            if(item.getThisBoolOnlyOnce == true)
            {
                
            }
        }
        RefreshCount();
        return;
   }
    public void ClearSlot ()
   {
       item = null;

       icon.sprite = null;
       icon.enabled = false;
       removeButton.interactable = false;
        //TESTING STACKING
        
        countText.gameObject.SetActive(false);
    }
   public void OnRemoveButton()
   {
       Inventory.instance.Remove(item);
   }

   public void UseItem()
   {
       if (item != null)
       {
           item.Use();
       }
   }
    //TESTING
    
    public void RefreshCount()
    {
        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.interactable = true;


        //TESTING STACKING
        countText.gameObject.SetActive(true);

        countText.text = item.itemAmount.ToString();
        bool textActive = item.itemAmount > 1; //Don't show amount if just 1.
        countText.gameObject.SetActive(textActive);
    }
    public void NoRefreshCount()
    {
        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;


        //TESTING STACKING
        countText.gameObject.SetActive(false);

        countText.text = item.itemAmount.ToString();
        bool textActive = item.itemAmount > 1; //Don't show amount if just 1.
        countText.gameObject.SetActive(textActive);
    }
    
    public void Holder()
    {
        var randomNumber = new int[5];

        randomNumber[0] = 1;
        randomNumber[1] = 2;
        randomNumber[2] = 3;
        randomNumber[3] = 4;
        randomNumber[4] = 5;
        index = UnityEngine.Random.Range(1, randomNumber.Length + 1);
        Debug.Log("Hey im using random numbers 1-5: " + index);
        counter += index;
        Debug.Log(counter);
    }
    
}
