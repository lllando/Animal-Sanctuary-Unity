using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Item _assignedItem;

    private SpriteRenderer _spriteRenderer;

    public Item AssignedItem
    {
        get { return _assignedItem; }
        set { _assignedItem = value; }
    }

    public SpriteRenderer SpriteRenderer
    {
        get { return _spriteRenderer; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.InventoryManager.AddItem(_assignedItem, 1);
            this.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
}
