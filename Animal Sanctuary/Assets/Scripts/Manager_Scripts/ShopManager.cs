using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Item focusItem;

    public ShopInteractive focusTrader;

    public ShopInteractive FocusTrader
    {
        get { return focusTrader; }
        set { focusTrader = value; }
    }

    public void BuyItem() //Via Inspector (Button)
    {
        //We checked if the player has enough coins before (button interactable condition)

        int itemCount = GameManager.InterfaceManager.ItemCount;

        int individualCost = focusItem.ItemCost;
        float totalCost = individualCost * itemCount;

        GameManager.InventoryManager.AddItem(focusItem, itemCount);
        GameManager.InventoryManager.RemoveItem(GameManager.InventoryManager.coinItem, (int)totalCost);

        focusTrader.RemoveItem(focusItem, itemCount);
    }

    public void SellItem(Item item)
    {
        GameManager.InventoryManager.AddItem(GameManager.InventoryManager.coinItem, item.ItemCost / 2);
        GameManager.InventoryManager.RemoveItem(item, 1);
        GameManager.InterfaceManager.UpdateInventory();
    }
}
