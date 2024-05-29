using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item assignedItem;

    [SerializeField] private int amount = 1;

    private SpriteRenderer _spriteRenderer;

    private AudioSource audioSource;

    [SerializeField] private AudioClip pickupAudioClip;

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
            GameManager.InventoryManager.AddItem(assignedItem, amount);
            AudioManager.Instance.PlayAudioUsingPrefab(gameObject.transform.position, pickupAudioClip, true);
            this.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
}
