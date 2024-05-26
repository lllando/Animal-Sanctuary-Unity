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
        GameManager.HabitatManager.AnimalSelectInventoryCheck(_assignedItem);
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
