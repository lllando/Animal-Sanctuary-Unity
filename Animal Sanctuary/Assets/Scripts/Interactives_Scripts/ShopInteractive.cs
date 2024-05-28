using UnityEngine;

public class ShopInteractive : MonoBehaviour, Interactive
{
    [SerializeField] private float interactDistanceThreshold;

    [SerializeField] private InventoryItem[] shopInventory = new InventoryItem[12];

    [SerializeField] private DialogueInteractive introDialogue;

    public float InteractDistanceThreshold
    {
        get { return interactDistanceThreshold; }
    }

    public InventoryItem[] ShopInventory
    {
        get { return shopInventory; }
    }

    public void Interact()
    {
        if(introDialogue != null)
        {
            GameManager.DialogueManager.StartDialogue(introDialogue);
            introDialogue = null;
        }
        else
        {
            OpenShop();
        }
    }

    public void OpenShop() //Via Event (Dialogue)
    {
        if (MeasurementUtility.IsNear(this.transform.position, GameManager.PlayerTransform.position, interactDistanceThreshold))
        {
            GameManager.ShopManager.focusTrader = this;
            GameManager.InterfaceManager.DisplayShop(shopInventory);
        }
    }

    public void RemoveItem(Item item, int stackSize)
    {
        foreach (InventoryItem inventoryItem in shopInventory)
        {
            if (inventoryItem.Item == item)
            {
                if (inventoryItem.StackSize >= stackSize)
                {
                    inventoryItem.StackSize -= stackSize;
                    Debug.Log("There was enough items in this stack to remove the indicated items!");

                    if (inventoryItem.StackSize == 0)
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

        if (stackSize > 0)
        {
            Debug.Log("The items/number of items did not exist in the inventory");
        }

        GameManager.InterfaceManager.DisplayShop(shopInventory);
    }
}
