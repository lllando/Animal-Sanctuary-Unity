using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int inventorySize = 12;

    [SerializeField] private Item itemTest;

    private List<InventoryItem> _inventory = new List<InventoryItem>();

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
        if(!ItemStackExists(item))
        {
            if(_inventory.Count < inventorySize)
            {
                _inventory.Add(new InventoryItem(item, stackSize));
            }
        }
        else
        {
            foreach (InventoryItem inventoryItem in _inventory)
            {
                if (inventoryItem.Item == item)
                {

                    int sum = inventoryItem.StackSize + stackSize;
                    int left = sum - item.ItemMaxStack;

                    if (inventoryItem.StackSize + stackSize > item.ItemMaxStack)
                    {
                        // int leftOvers = in item.ItemMaxStack - stackSize;
                        inventoryItem.StackSize = item.ItemMaxStack;

                        if (left > 0)
                        {
                            AddItem(item, left);
                        }
                    }
                    else
                    {
                        inventoryItem.StackSize = stackSize;
                    }

                    break;
                }
            }
        }

        GameManager.InterfaceManager.UpdateInventory(_inventory);
    }

    public void RemoveItem(Item item, int stackSize)
    {
        if(ItemExists(item, stackSize))
        {
            int carryOver = 0;

            for (int i = 0; i < _inventory.Count; i++)
            {
                InventoryItem inventoryItem = _inventory[i];

                if (_inventory[i].Item == item)
                {
                    int total = 0;

                    if(carryOver == 0)
                    {
                        total = inventoryItem.StackSize;
                    }
                    else
                    {
                        total = carryOver;
                    }

                    total -= stackSize;

                    if (total == 0)
                    {
                        _inventory.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        inventoryItem.StackSize -= stackSize;
                        carryOver = total;
                    }
                }
            }

            GameManager.InterfaceManager.UpdateInventory(_inventory);
        }
    }

    private bool ItemStackExists(Item checkItem)
    {
        foreach(InventoryItem item in _inventory)
        {
            if(checkItem == item.Item)
            {
                if(item.StackSize < checkItem.ItemMaxStack)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool ItemExists(Item item, int itemStack)
    {
        int total = 0;

        foreach(InventoryItem inventoryItem in _inventory)
        {
            total += _inventory.Count;

            if(total >= itemStack)
            {
                return true;
            }
        }

        return false;
    }
}

public class InventoryItem
{
    private Item _item;

    private int _stackSize;

    public Item Item
    {
        get { return _item; }
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
