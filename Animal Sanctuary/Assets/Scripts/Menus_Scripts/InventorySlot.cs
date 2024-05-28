using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Image _itemIcon;

    private TextMeshProUGUI _itemCountText;

    private Button _thisButton;

    private Item _assignedItem;

    public void Initialise()
    {
        _itemIcon = this.transform.GetChild(0).GetComponent<Image>();
        _itemCountText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        _thisButton = this.GetComponent<Button>();
        _thisButton.onClick.AddListener(OnInventorySlotClick);
    }

    private void OnInventorySlotClick()
    {
        if (_assignedItem == null)
        {
            return;
        }

        switch (InterfaceManager.ActiveItemInteraction)
        {
            case InterfaceManager.ItemInteraction.Animal:

                if(_assignedItem.ItemTagList.Contains("Animal"))
                {
                    GameManager.HabitatManager.AnimalSelectInventoryCheck(_assignedItem);
                }
                
                break;
            case InterfaceManager.ItemInteraction.Food:

                if(_assignedItem.ItemTagList.Contains("Food"))
                {
                    GameManager.HabitatManager.UpdateAnimalStatistic(0, _assignedItem);
                }
               
                break;
            case InterfaceManager.ItemInteraction.Water:

                if(_assignedItem.ItemTagList.Contains("Water"))
                {
                    GameManager.HabitatManager.UpdateAnimalStatistic(1, _assignedItem);
                }
                
                break;
            case InterfaceManager.ItemInteraction.Medicine:

                if(_assignedItem.ItemTagList.Contains("Medicine"))
                {
                    GameManager.HabitatManager.UpdateAnimalStatistic(2, _assignedItem);
                }
                
                break;
            case InterfaceManager.ItemInteraction.Sell:

                GameManager.ShopManager.SellItem(_assignedItem);

                break;
        }
    }

    public void AssignItem(Item item, int stackSize)
    {
        _assignedItem = item;
        
        if(item != null)
        {
            _itemIcon.gameObject.SetActive(true);
            _itemIcon.sprite = item.ItemIcon;
            _itemCountText.text = stackSize.ToString();
        }
        else
        {
            _itemIcon.gameObject.SetActive(false);
            _itemIcon.sprite = null;
            _itemCountText.text = "00";
        }
    }
}
