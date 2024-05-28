using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TraderSlot : MonoBehaviour
{
    private Item _assignedItem;
    private int _stackSize;

    private Button _thisButton;
    private Image _itemIcon;
    private TextMeshProUGUI _stackSizeText;

    public Item AssignedItem
    {
        get { return _assignedItem; }
    }

    private void Awake()
    {
        _thisButton = this.GetComponent<Button>();

        _itemIcon = this.transform.GetChild(0).GetComponent<Image>();
        _stackSizeText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        _thisButton.onClick.AddListener(OnSlotClick);
    }

    private void OnSlotClick()
    {
        if(_assignedItem != null)
        {
            GameManager.ShopManager.focusItem = _assignedItem;
            GameManager.InterfaceManager.DisplayBuyItem(_assignedItem, _stackSize);
        }
    }

    public void AssignItem(Item item, int stack)
    {
        _assignedItem = item;
        _stackSize = stack;

        if(item == null)
        {
            _itemIcon.sprite = null;
            _itemIcon.gameObject.SetActive(false);
            _stackSizeText.text = "00";
        }
        else
        {
            _itemIcon.gameObject.SetActive(true);
            _stackSizeText.text = stack.ToString();
            _itemIcon.sprite = item.ItemIcon;
        }
    }
}
