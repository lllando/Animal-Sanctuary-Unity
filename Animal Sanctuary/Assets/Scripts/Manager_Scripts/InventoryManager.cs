using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Item[] itemArray;

    public Item coinItem;

    public InventoryItem[] Inventory
    {
        get { return GameManager.SaveManager.inventory; }
    }

    public int Coins
    {
        get { return GetItemCount(coinItem); }
    }

    public void AddItem(Item item, int stackSize)
    {
        if(stackSize == 0)
        {
            return;
        }

        foreach(InventoryItem inventoryItem in Inventory)
        {
            if(inventoryItem.Item == item)
            {
                if(inventoryItem.StackSize + stackSize <= item.ItemMaxStack)
                {
                    Debug.Log("An item exists! And there is space!");
                    inventoryItem.StackSize += stackSize;
                    GameManager.InterfaceManager.UpdateInventory();
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

        foreach(InventoryItem check in Inventory)
        {
            if(check.Item == null)
            {
                check.Item = item;
                check.StackSize = stackSize;
                Debug.Log("An inventory slot was free, so we added an item!");
                GameManager.InterfaceManager.UpdateInventory();
                return;
            }
        }

        Debug.Log("Inventory was full!");
    }

    public void RemoveItem(Item item, int stackSize)
    {
        foreach(InventoryItem inventoryItem in Inventory)
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

        GameManager.InterfaceManager.UpdateInventory();
    }

    public bool HasItem(Item item, int total)
    {
        int totalItem = 0;

        foreach(InventoryItem inventoryItem in Inventory)
        {
            if(inventoryItem.Item == item)
            {
                totalItem += inventoryItem.StackSize;
            }
        }

        return totalItem >= total;
    }

    public int GetItemCount(Item item)
    {
        int totalItem = 0;

        foreach (InventoryItem inventoryItem in Inventory)
        {
            if (inventoryItem.Item == item)
            {
                totalItem += inventoryItem.StackSize;
            }
        }

        return totalItem;
    }

    public void BuyItem()
    {

    }
}

[System.Serializable]
public class InventoryItem
{
    [SerializeField] private Item _item;

    [SerializeField] private int _stackSize;

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
