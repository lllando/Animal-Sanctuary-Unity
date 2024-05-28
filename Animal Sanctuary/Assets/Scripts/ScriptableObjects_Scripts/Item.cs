using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObject/Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;

    [SerializeField] private Sprite itemIcon;

    [SerializeField] private int itemCost;

    [SerializeField] private int itemMaxStack = 1;

    [SerializeField] private Vector3 itemScale;

    [SerializeField] private int itemUseIncrease;

    [SerializeField] private List<string> itemTagList;

    [SerializeField] private int itemId;

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

    public Vector3 ItemScale
    {
        get { return itemScale; }
    }

    public int ItemUseIncrease
    {
        get { return itemUseIncrease; }
    }

    public List<string> ItemTagList
    {
        get { return itemTagList; }
    }

    public int ItemID
    {
        get { return itemId; }
    }
}
