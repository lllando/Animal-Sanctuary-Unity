using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item assignedItem;

    private SpriteRenderer _spriteRenderer;

    public Item AssignedItem
    {
        get { return assignedItem; }
        set { assignedItem = value; }
    }

    public SpriteRenderer SpriteRenderer
    {
        get { return _spriteRenderer; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.InventoryManager.AddItem(assignedItem, 1);
            this.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
}
