using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Item itemTest;

    private InventoryItem[] _inventory = new InventoryItem[12];

    private void Awake()
    {
        for(int i = 0; i < 12; i++)
        {
            _inventory[i] = new InventoryItem(null, 0);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            AddItem(itemTest, 1);
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            RemoveItem(itemTest, 1);
        }
    }

    public void AddItem(Item item, int stackSize)
    {
        foreach(InventoryItem inventoryItem in _inventory)
        {
            if(inventoryItem.Item == item)
            {
                if(inventoryItem.StackSize + stackSize <= item.ItemMaxStack)
                {
                    Debug.Log("An item exists! And there is space!");
                    inventoryItem.StackSize += stackSize;
                    GameManager.InterfaceManager.UpdateInventory(_inventory);
                    return;
                }
                else
                {
                    Debug.Log("An item exists, but there is not enough space!");
                    int room = item.ItemMaxStack - inventoryItem.StackSize;
                    stackSize -= room;

                    inventoryItem.StackSize = item.ItemMaxStack;
                    Debug.Log("There was some items left over, so we are going to try and add the item again");
                }
            }
        }

        Debug.Log("An item was not found, so just add a new inventory item");

        foreach(InventoryItem check in _inventory)
        {
            if(check.Item == null)
            {
                check.Item = item;
                check.StackSize = stackSize;
                Debug.Log("An inventory slot was free, so we added an item!");
                GameManager.InterfaceManager.UpdateInventory(_inventory);
                return;
            }
        }

        Debug.Log("Inventory was full!");
    }

    public void RemoveItem(Item item, int stackSize)
    {
        foreach(InventoryItem inventoryItem in _inventory)
        {
            if(inventoryItem.Item == item)
            {
                if(inventoryItem.StackSize >= stackSize)
                {
                    inventoryItem.StackSize -= stackSize;
                    Debug.Log("There was enough items in this stack to remove the indicated items!");

                    if(inventoryItem.StackSize == 0)
                    {
                        Debug.Log("Stack was emptied!");
                        inventoryItem.Item = null;
                    }

                    break;
                }
                else
                {
                    stackSize -= inventoryItem.StackSize;
                    inventoryItem.StackSize = 0;
                    inventoryItem.Item = null;
                    Debug.Log("This stack is now empty, but we want to continue to remove this item.");
                    continue;
                }
            }
        }

        if(stackSize > 0)
        {
            Debug.Log("The items/number of items did not exist in the inventory");
        }

        GameManager.InterfaceManager.UpdateInventory(_inventory);
    }
}

public class InventoryItem
{
    private Item _item;

    private int _stackSize;

    public Item Item
    {
        get { return _item; }
        set { _item = value; }
    }

    public int StackSize
    {
        get { return _stackSize; }
        set { _stackSize = value; }
    }

    public InventoryItem(Item item, int stackSize)
    {
        _item = item;
        _stackSize = stackSize;
    }
}
