//using FMOD.Studio;
//using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI; 
    Inventory inventory;

    public InventorySlot[] slots;
    InventorySlot inventorySlot;
    DragItem dragItem;
    public Item itemItem;
    [NonSerialized]
    int counter = 0;
    [NonSerialized]
    int index = 0;
    [NonSerialized]
    public int exceedingAmount2 = 0;
    [NonSerialized]
    public int exceedingAmount3 = 0;
    //int counter2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventorySlot = itemsParent.GetComponentInChildren<InventorySlot>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        
    }
    public void UpdateUI()
    {
        //CHECK IF SLOT IS NULL
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                //Debug.Log("Slots that are empty:" + inventory.items[i].ID);
                //Debug.Log("i:n arvo on: " + i);
                if (slots[i].item != null)
                {
                    continue;
                }
                if (slots[i].item == null)
                {

                    slots[i].item = inventory.items[i];
                    slots[i].countText.gameObject.SetActive(true);
                    slots[i].countText.text = slots[i].item.itemAmount.ToString();
                    slots[i].RefreshCount();
                    return;
                }
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
        /*
        for (int i = 0; i < slots.Length; i++)
        {
            
                if (i < inventory.items.Count)
                { 
                slots[i].AddItem(inventory.items[i]);
                slots[i].countText.gameObject.SetActive(true);
                slots[i].countText.text = slots[i].item.itemAmount.ToString();
                slots[i].RefreshCount();
                    
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
    */
        /*
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
                Debug.Log("i:n arvo on: " + i);

                if (slots[i].item.isStackable == true)
                {
                    slots[i].countText.gameObject.SetActive(true);
                    slots[i].countText.text = slots[i].item.itemAmount.ToString();
                        Debug.Log(inventory.items[i].GetInstanceID());
                        Debug.Log(slots[i].item.GetInstanceID());
                    //int test = inventory.items.FindIndex(o => o.name == slots[i].item.name);
                    int test = inventory.items.FindIndex(o => o.name == slots[i].item.name);
                    Debug.Log("test: " + test);
                    if (test != i)
                    {
                        if (slots[test].item.GetInstanceID() == inventory.items[i].GetInstanceID())
                        {
                            Debug.Log("FOUND");

                            Holder();
                            slots[test].item.itemAmount += counter;
                            //IF we find another same stackable object, delete it.
                            inventory.items.RemoveAt(i);
                            slots[i].ClearSlot();
                            //slots[inventory.items.FindIndex(o => o.name == slots[i].item.name)].ClearSlot();
                            slots[test].RefreshCount();
                            //inventorySlot = itemsParent.GetComponentInChildren<InventorySlot>();
                            //inventorySlot.RefreshCount();
                            //slots[test].item.getThisBoolOnlyOnce = true;

                            if (slots[test].item.itemAmount < slots[test].item.maxStacks)
                            {
                                Debug.Log("Value of the first stackable is lower than max stacks");

                            }
                            if (slots[test].item.itemAmount > slots[test].item.maxStacks)
                            {

                                Debug.Log("Max stacks reached");
                                int exceedingAmount = slots[test].item.itemAmount - slots[test].item.maxStacks;
                                Debug.Log(exceedingAmount);
                                itemItem = Instantiate(slots[test].item);
                                inventory.items.Add(itemItem);
                                itemItem.itemAmount = exceedingAmount;

                                slots[i].AddItem(inventory.items[i]);
                                //int test2 = inventory.items.FindIndex(o => o.name == slots[test].item.name);
                                int test2 = inventory.items.FindLastIndex(o => o.name == slots[i].item.name);
                                Debug.Log(test2);

                                Debug.Log(slots[i].item.itemAmount);
                                Debug.Log(slots[test].item.itemAmount);

                                //slots[test].countText.gameObject.SetActive(true);
                                //slots[test].countText.text = itemItem.itemAmount.ToString();

                                slots[test].item.itemAmount = slots[test].item.maxStacks;
                                slots[test].RefreshCount();
                                slots[test2].RefreshCount();

                                //if (slots[test2].item.itemAmount < slots[test2].item.maxStacks)
                                if (itemItem.itemAmount < itemItem.maxStacks)
                                {
                                    Debug.Log("Lower than max stacks again");
                                    Debug.Log(itemItem.itemAmount);
                                    //int test3 = inventory.items.FindLastIndex(o => o.name == slots[i].item.name);
                                    Debug.Log(test);
                                    Debug.Log(test2);
                                    //Debug.Log(test3);

                                    //slots[test2].item.itemAmount += slots[i].item.itemAmount;
                                    
                                    //if (slots[test2].item != null)
                                    {
                                        inventory.items.RemoveAt(test2);
                                        slots[i].ClearSlot();
                                    }
                                    Debug.Log(slots[i].item.itemAmount);
                                    Debug.Log(itemItem.itemAmount);
                                    Debug.Log(slots[test].item.itemAmount);
                                    Debug.Log(slots[test2].item.itemAmount);
                                    //Debug.Log(slots[test3].item.itemAmount);

                                    slots[test].RefreshCount();
                                    slots[test2].RefreshCount();
                                    //slots[test3].RefreshCount();
                                }
                                if (slots[test2].item.itemAmount > slots[test2].item.maxStacks)
                                {
                                    Debug.Log("Max stacks reached again");
                                    exceedingAmount2 = slots[test2].item.itemAmount - slots[test2].item.maxStacks;
                                    Debug.Log(exceedingAmount2);

                                    itemItem2 = Instantiate(slots[test2].item);
                                    inventory.items.Add(itemItem2);
                                    itemItem2.itemAmount = exceedingAmount2;

                                    slots[i].AddItem(inventory.items[i]);
                                    //int test2 = inventory.items.FindIndex(o => o.name == slots[i].item.name);
                                    int test3 = inventory.items.FindLastIndex(o => o.name == slots[i].item.name);
                                    Debug.Log(test3);

                                    //slots[test].countText.gameObject.SetActive(true);
                                    //slots[test].countText.text = itemItem.itemAmount.ToString();

                                    slots[test2].item.itemAmount = slots[test2].item.maxStacks;
                                    slots[test2].RefreshCount();
                                    slots[test3].RefreshCount();
                                }
                            }
                        }

                    }
                    if(test == i)
                    {
                        if (slots[test].item.isStackable == true)
                        {
                            if (slots[test].item.itemAmount == 0)
                            {
                                Holder();
                                slots[test].item.itemAmount += counter;
                                slots[test].RefreshCount();
                            }
                        }
                    }
                    //inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                    //slots[inventory.items.FindIndex(o => o.name == slots[i].item.name)].ClearSlot();
                    //slots[i].icon.sprite = slots[i].item.icon;
                    //slots[i].countText.gameObject.SetActive(true);
                    //slots[i].countText.text = slots[i].item.itemAmount.ToString();
                    //slots[i].AddItem(inventory.items[i]);
                }
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }*/

        /* 
         for (int i = 0; i < slots.Length; i++)
         {
             if (i < inventory.items.Count)
             {
                 Debug.Log("hei");
                 inventorySlot = itemsParent.GetComponentInChildren<InventorySlot>();
                 inventorySlot.RefreshCount();
             }
             else
             {
                 slots[i].ClearSlot();
             }
         }
     }*/
        //int test = inventory.items.FindIndex(o => o.name == slots[i].item.name);
        //Debug.Log("Paikka: " + i);
        //Debug.Log("test: " + test);
        //if (test != i)
        //{
        /*
            if (slots[i].item.itemAmount < slots[i].item.maxStacks)
            {
                Holder();
                slots[i].item.itemAmount += counter;
                slots[i].countText.gameObject.SetActive(true);
                slots[i].countText.text = slots[i].item.itemAmount.ToString();

                //inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));

            //slots[inventory.items.FindIndex(o => o.name == slots[i].item.name)].ClearSlot();
            Debug.Log("clearattu");

            slots[i].countText.gameObject.SetActive(true);
            slots[i].countText.text = slots[i].item.itemAmount.ToString();

            }
            //inventory.items.RemoveAt(i);

    //} */
        /*
       Holder();
       slots[i].item.itemAmount += counter;
       slots[i].countText.gameObject.SetActive(true);
       slots[i].countText.text = slots[i].item.itemAmount.ToString();
    */

        //slots[i].countText.gameObject.SetActive(true);
        //slots[i].countText.text = slots[i].item.itemAmount.ToString();

        //if (inventory.items[i].GetInstanceID() == slots[i].item.GetInstanceID())


        /*
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);

                if (slots[i].item.isStackable == true)
                {
                        if (inventory.items[i].GetInstanceID() == slots[i].item.GetInstanceID())
                        {
                        Debug.Log("----");
                        if (slots[i].item.getThisBoolOnlyOnce == true)
                            {
                                Debug.Log("---");

                                slots[i].item.getThisBoolOnlyOnce= false;

                                Holder();
                                slots[i].item.itemAmount += counter;

                                slots[i].countText.gameObject.SetActive(true);
                                slots[i].countText.text = slots[i].item.itemAmount.ToString();



                            //inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                            //inventory.items.Remove();
                            if (slots[i].item.itemAmount > slots[i].item.maxStacks)
                            {

                                Debug.Log("PAST the cap");
                                itemItem = Instantiate(slots[i].item);
                                inventory.items.Add(itemItem);
                                itemItem.getThisBoolOnlyOnce = false;

                                itemItem.itemAmount += counter;
                                slots[i].countText.text = itemItem.itemAmount.ToString();
                                inventory.items[i].itemAmount = inventory.items[i].maxStacks;
                                //slots[i].AddItem(itemItem);
                                //break;
                            }
                        }
                        /*if (inventory.items[i].GetInstanceID() != slots[i].item.GetInstanceID())
                        {
                            if (slots[i].item.getThisBoolOnlyOnce == false)
                            {
                                itemItem3 = Instantiate(slots[i].item);
                                inventory.items.Add(itemItem3);

                                inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                            }
                        }

                            //inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                        }
                }
            }


        }*/

        //counter = inventory.items.Count - 1;
        /*for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
                //Debug.Log(inventory.items.Count);
                //Debug.Log( < inventory.items.Count);

                if (slots[i].item.isStackable == true)
                {
                    int test = inventory.items.FindIndex(o => o.name == slots[i].item.name);
                    if (test != i)
                    {
                        Debug.Log("-----");
                        Debug.Log(inventory.items[i]);
                        Debug.Log(inventory.items[i].GetInstanceID());
                        Debug.Log(slots[i].item.GetInstanceID());
                        Debug.Log(slots[i].item);
                        if (inventory.items[i].GetInstanceID() != slots[i].item.GetInstanceID())
                        {
                            Debug.Log("-----!");
                            //slots[inventory.items.FindIndex(o => o.name == slots[i].item.name)].ClearSlot();
                            //inventory.items.RemoveAt(i);

                            Debug.Log("Tässä i on: " + i);
                            Debug.Log("Mikä itemi: " + slots[i].item);
                            Debug.Log(slots[i].item.getThisBoolOnlyOnce);
                            if (slots[i].item.getThisBoolOnlyOnce == false)
                            {
                                //inventorySlot = itemsParent.GetComponentInChildren<InventorySlot>();
                                //inventorySlot.Holder();
                                Holder();
                                Debug.Log("rolled number: " + counter);//inventorySlot.counter);

                                slots[i].item.itemAmount += counter;//inventorySlot.counter;

                                Debug.Log(inventory.items.FindIndex(o => o.name == slots[i].item.name));
                                Debug.Log("i arvo on: " + i);
                                /*if (slots[i].item.itemAmount >= slots[i].item.maxStacks)
                                {
                                    Debug.Log("max stacks reached");
                                    itemItem = Instantiate(slots[i].item);
                                    inventory.items.Add(itemItem);
                                    Debug.Log(itemItem);
                                    Debug.Log(itemItem.itemAmount);

                                    int exceedingAmount = slots[i].item.itemAmount - slots[i].item.maxStacks;
                                    Debug.Log("exceeding amount: " + exceedingAmount2);
                                    itemItem.itemAmount = exceedingAmount2;
                                    //slots[i + 1].item = itemItem; //EHKÄ!!!!
                                    //inventory.items.RemoveAt(i += 1);
                                    //continue;

                                } //päättyy
                            }
                        }
                        if (inventory.items[i].GetInstanceID() == slots[i].item.GetInstanceID())
                        {
                            Debug.Log("TESTATAAN ");
                            if (slots[i].item.itemAmount < slots[i].item.maxStacks)
                            {
                                Debug.Log("stackit ei ollu täys");
                                Debug.Log(slots[i].item.itemAmount);
                                exceedingAmount3 = slots[i].item.maxStacks - slots[i].item.itemAmount;
                                slots[i].item.itemAmount = slots[i].item.itemAmount + slots[i].item.itemAmount;
                                Debug.Log(slots[i].item.itemAmount);
                                inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                                itemItem = Instantiate(slots[i].item);
                                inventory.items.Add(itemItem);
                                Debug.Log(exceedingAmount3);
                                itemItem.itemAmount = exceedingAmount3;
                                itemItem.getThisBoolOnlyOnce = false;
                                Debug.Log(itemItem);
                                Debug.Log(itemItem.itemAmount);
                                Debug.Log("alotetaan uus rolli");
                                if (itemItem.getThisBoolOnlyOnce == false)
                                {
                                    Holder();
                                    Debug.Log("ROLLATTIIN numero: " + counter);//inventorySlot.counter);
                                    itemItem.itemAmount += counter;//inventorySlot.counter;
                                    Debug.Log(itemItem.itemAmount);

                                    if (itemItem.itemAmount < itemItem.maxStacks)
                                    {
                                        Debug.Log("JOS esim 1");
                                        itemItem2 = Instantiate(itemItem);
                                        inventory.items.Add(itemItem2);

                                        slots[i].AddItem(inventory.items[i]);
                                        //slots[i += 1].AddItem(itemItem2);

                                        break;
                                    }
                                    if (itemItem.itemAmount > itemItem.maxStacks)
                                    {
                                        int exceedingAmount = itemItem.itemAmount - itemItem.maxStacks;
                                        exceedingAmount2 = exceedingAmount;
                                        Debug.Log("tämä ON exceeding: " + exceedingAmount);
                                        //Debug.Log("exceeding amount: " + exceedingAmount);
                                        //slots[i].item.itemAmount = slots[i].item.itemAmount - exceedingAmount;
                                        itemItem.itemAmount = itemItem.maxStacks;
                                        itemItem2 = Instantiate(itemItem);
                                        inventory.items.Add(itemItem2);

                                        //inventory.items[i].itemAmount = inventory.items[i].itemAmount - exceedingAmount;
                                        itemItem2.itemAmount = exceedingAmount; //exceedingAmount - itemItem2.maxStacks;
                                        itemItem2.getThisBoolOnlyOnce = false;
                                        Debug.Log(itemItem2.itemAmount);
                                        Debug.Log(inventory.items[i].itemAmount);
                                        inventory.items[i].itemAmount = inventory.items[i].maxStacks;
                                        Debug.Log(slots[i].item.itemAmount);
                                        Debug.Log(itemItem.getThisBoolOnlyOnce);
                                        Debug.Log("Saako poistaa?");
                                        //inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                                        if (itemItem2.itemAmount == 0)
                                        {
                                            Debug.Log("poistetaan nolla");
                                            //inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                                        }
                                        //slots[i].AddItem(inventory.items[i]);
                                        //slots[i += 1].AddItem(itemItem2);

                                        if(itemItem2.itemAmount > itemItem2.maxStacks)
                                        {
                                            Debug.Log("joo täs ollaan");
                                            itemItem2.itemAmount = itemItem2.maxStacks;
                                            itemItem3 = Instantiate(itemItem2);
                                            inventory.items.Add(itemItem3);
                                            itemItem3.getThisBoolOnlyOnce = false;
                                            Debug.Log(exceedingAmount);
                                            itemItem3.itemAmount = exceedingAmount;

                                            if(itemItem3.itemAmount > itemItem3.maxStacks)
                                            {
                                                itemItem3.itemAmount = itemItem3.itemAmount - itemItem3.maxStacks;
                                                itemItem4 = Instantiate(itemItem3);
                                                inventory.items.Add(itemItem4);
                                                itemItem4.getThisBoolOnlyOnce = false;
                                                itemItem4.itemAmount = itemItem3.itemAmount;
                                                itemItem3.itemAmount = itemItem3.maxStacks;
                                            }

                                        }
                                        if (itemItem2.itemAmount < itemItem2.maxStacks)
                                        {
                                            Debug.Log("joo tässä sitä taas ollaan");

                                            itemItem2.itemAmount = itemItem2.maxStacks;
                                            itemItem3 = Instantiate(itemItem2);

                                            inventory.items.Add(itemItem3);
                                            Debug.Log(exceedingAmount);
                                            itemItem3.itemAmount = exceedingAmount;

                                            slots[i].AddItem(inventory.items[i]);
                                            //inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));

                                        }
                                        if (itemItem2.itemAmount == itemItem2.maxStacks)
                                        {
                                            Debug.Log("jaahasjaahas");
                                            itemItem2.itemAmount = itemItem2.maxStacks;
                                            itemItem3 = Instantiate(itemItem2);
                                            inventory.items.Add(itemItem3);
                                            Debug.Log(itemItem2.itemAmount);
                                            Debug.Log(itemItem3.itemAmount);
                                            //inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                                            //break;
                                        }
                                        //inventory.items.RemoveAt(i += 1);
                                        //break;
                                    }
                                }

                                if (slots[i].item.itemAmount == slots[i].item.maxStacks)
                                {
                                    Debug.Log("pitääkö poistaa?");
                                    itemItem2 = Instantiate(itemItem);
                                    inventory.items.Add(itemItem2);
                                    itemItem2.itemAmount = itemItem2.maxStacks;
                                    itemItem2.getThisBoolOnlyOnce = false;

                                    inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                                    break;
                                }
                            }
                                if (slots[i].item.itemAmount == slots[i].item.maxStacks)
                            {
                                Debug.Log("stackit täynnä");
                                //itemItem = Instantiate(slots[i].item);
                                if(itemItem != null)
                                {
                                    Debug.Log(itemItem.GetInstanceID());
                                    Debug.Log(itemItem.itemAmount);

                                    itemItem2 = Instantiate(itemItem);
                                    inventory.items.Add(itemItem2);
                                    inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                                    itemItem2.getThisBoolOnlyOnce = false;
                                    Debug.Log(itemItem2);
                                    Debug.Log(itemItem2.itemAmount);

                                    if (itemItem2.getThisBoolOnlyOnce == false)
                                    {
                                        Holder();
                                        Debug.Log("ROLLED number: " + counter);//inventorySlot.counter);

                                        itemItem2.itemAmount += counter;//inventorySlot.counter;
                                        Debug.Log(itemItem2.itemAmount);

                                        if(itemItem2.itemAmount < itemItem2.maxStacks)
                                        {
                                            Debug.Log("Jos esim 1");
                                            //itemItem = Instantiate(itemItem2);
                                            inventory.items.Add(itemItem2);
                                            inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                                            //slots[i].AddItem(inventory.items[i]);
                                            //slots[i += 1].AddItem(itemItem2);

                                            break;
                                        }
                                        if (itemItem2.itemAmount > itemItem2.maxStacks)
                                        {
                                            int exceedingAmount = itemItem2.itemAmount - itemItem2.maxStacks;
                                            exceedingAmount2 = exceedingAmount;
                                            Debug.Log("TÄMÄ on exceeding: " + exceedingAmount);
                                            //Debug.Log("exceeding amount: " + exceedingAmount);
                                            //slots[i].item.itemAmount = slots[i].item.itemAmount - exceedingAmount;
                                            itemItem2.itemAmount = itemItem2.maxStacks;
                                            itemItem = Instantiate(itemItem);
                                            inventory.items.Add(itemItem);

                                            inventory.items[i].itemAmount = inventory.items[i].itemAmount - exceedingAmount;
                                            itemItem.itemAmount = exceedingAmount;
                                            itemItem.getThisBoolOnlyOnce = false;
                                            Debug.Log(itemItem.itemAmount);
                                            Debug.Log(inventory.items[i].itemAmount);
                                            inventory.items[i].itemAmount = inventory.items[i].maxStacks;
                                            Debug.Log(slots[i].item.itemAmount);
                                            Debug.Log(itemItem.getThisBoolOnlyOnce);

                                            slots[i].AddItem(inventory.items[i]);
                                            slots[i += 1].AddItem(itemItem);
                                            //inventory.items.RemoveAt(i += 1);
                                            break;
                                        }
                                        if (itemItem2.itemAmount == itemItem2.maxStacks)
                                        {
                                            Debug.Log("JUSTJUST");
                                            break;
                                        }
                                    }

                                }
                                //NÄILLÄ TIEDOILLA LASKE EXCEEDING AMOUNT KOPIOON JA SIT TULEVA

                                itemItem = Instantiate(slots[i].item);
                                inventory.items.Add(itemItem);
                                inventory.items.RemoveAt(inventory.items.LastIndexOf(slots[i].item));
                                itemItem.getThisBoolOnlyOnce = false;
                                if(itemItem.getThisBoolOnlyOnce == false)
                                {
                                    Holder();
                                    Debug.Log("ROLLeeed number: " + counter);//inventorySlot.counter);

                                    itemItem.itemAmount += counter;//inventorySlot.counter;

                                    if(itemItem.itemAmount > itemItem.maxStacks)
                                    {
                                        int exceedingAmount = itemItem.itemAmount - itemItem.maxStacks;
                                        exceedingAmount2 = exceedingAmount;
                                        Debug.Log("TÄMÄääää on exceeding: " + exceedingAmount);
                                        //Debug.Log("exceeding amount: " + exceedingAmount);
                                        //slots[i].item.itemAmount = slots[i].item.itemAmount - exceedingAmount;
                                        itemItem = Instantiate(itemItem);
                                        inventory.items.Add(itemItem);

                                        inventory.items[i].itemAmount = inventory.items[i].itemAmount - exceedingAmount;
                                        itemItem.itemAmount = exceedingAmount;
                                        itemItem.getThisBoolOnlyOnce = false;
                                        Debug.Log(itemItem.itemAmount);
                                        Debug.Log(inventory.items[i].itemAmount);
                                        Debug.Log(slots[i].item.itemAmount);
                                        Debug.Log(itemItem.getThisBoolOnlyOnce);

                                        slots[i].AddItem(inventory.items[i]);
                                        slots[i += 1].AddItem(itemItem);
                                        //inventory.items.RemoveAt(i += 1);
                                    }
                                }


                                //Debug.Log(inventory.items[i+=1].itemAmount);
                                //Debug.Log(slots[i+=1].item.itemAmount);
                            }
                        }
                    }

                }

            }
        }*/

        public void Holder()
    {
        counter = 0;
        var randomNumber = new int[5];

        randomNumber[0] = 1;
        randomNumber[1] = 2;
        randomNumber[2] = 3;
        randomNumber[3] = 4;
        randomNumber[4] = 5;
        index = UnityEngine.Random.Range(1, randomNumber.Length); //+ 1
        Debug.Log("Hey im using random numbers 1-5: " + index);
        counter += index;
        Debug.Log("counterin arvo: " + counter);
    }
}
