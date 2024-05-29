using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterInteractive : MonoBehaviour, Interactive
{
    [SerializeField] private float interactDistanceThreshold;

    [SerializeField] private Item bucketItem;

    [SerializeField] private Item waterBucketItem;

    public float InteractDistanceThreshold
    {
        get { return interactDistanceThreshold; }
    }

    public void Interact()
    {
        if (MeasurementUtility.IsNear(this.transform.position, GameManager.PlayerTransform.position, interactDistanceThreshold))
        {
            if(GameManager.InventoryManager.HasItem(bucketItem, 1))
            {
                GameManager.InventoryManager.AddItem(waterBucketItem, 1);
                GameManager.InventoryManager.RemoveItem(bucketItem, 1);
            }

            Debug.Log("Water Interact");
        }
    }
}
