using UnityEngine;

public class ResourceInteractive : MonoBehaviour, Interactive
{
    [SerializeField] private Item item;

    [SerializeField] private int itemCount;

    public void Interact()
    {
        GameManager.InventoryManager.AddItem(item, itemCount);
        Destroy(this.gameObject);
    }
}

public interface Interactive
{
    public void Interact();
}
