using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ItemPickup itemPickupPrefab;

    private List<ItemPickup> _spawnedResources = new List<ItemPickup>();

    public void SpawnResource(Item itemType, Vector2 positon)
    {
        ItemPickup itemPickup = null;

        foreach(ItemPickup pickup in _spawnedResources)
        {
            if(!pickup.gameObject.activeSelf)
            {
                pickup.gameObject.SetActive(true);
                itemPickup = pickup;
                break;
            }
        }

        if(itemPickup == null)
        {
            itemPickup = Instantiate(itemPickupPrefab, Vector2.zero, Quaternion.identity);
            _spawnedResources.Add(itemPickup);
        }

        float minX = positon.x - 1.2f;
        float minY = positon.y - 1.2f;

        float maxX = positon.x + 1.2f;
        float maxY = positon.y + 1.2f;

        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        itemPickup.AssignedItem = itemType;
        itemPickup.transform.position = randomPosition;
        itemPickup.transform.localScale = itemType.ItemScale;
        itemPickup.SpriteRenderer.sprite = itemType.ItemIcon;
    }
}
