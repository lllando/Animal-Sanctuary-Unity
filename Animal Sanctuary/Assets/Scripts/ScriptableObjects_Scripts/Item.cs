using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObject/Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;

    [SerializeField] private Sprite itemIcon;

    [SerializeField] private int itemCost;

    [SerializeField] private int itemMaxStack = 1;

    public string ItemName
    {
        get { return itemName; }
    }

    public Sprite ItemIcon
    {
        get { return itemIcon; }
    }

    public int ItemCost
    {
        get { return itemCost; }
    }

    public int ItemMaxStack
    {
        get { return itemMaxStack; }
    }
}
