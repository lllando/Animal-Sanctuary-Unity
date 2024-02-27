using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private Image[] inventoryIcon;

    [SerializeField] private TextMeshProUGUI[] inventoryCountText;

    public void UpdateInventory(List<InventoryItem> inventoryList)
    {
        int index = 0;

        foreach(InventoryItem item in inventoryList)
        {
            inventoryIcon[index].sprite = item.Item.ItemIcon;
            inventoryCountText[index].text = item.StackSize.ToString();

            index++;
        }
    }
}
