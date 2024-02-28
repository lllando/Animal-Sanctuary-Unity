using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private Image[] inventoryIcon;

    [SerializeField] private TextMeshProUGUI[] inventoryCountText;

    public void UpdateInventory(InventoryItem[] inventory)
    {
        int index = 0;

        foreach(InventoryItem item in inventory)
        {
            if (item.Item != null)
            {
                inventoryIcon[index].sprite = item.Item.ItemIcon;
                inventoryCountText[index].text = item.StackSize.ToString();
            }
            else
            {
                inventoryIcon[index].sprite = null;
                inventoryCountText[index].text = "0";
            }

            index++;
        }
    }
}
