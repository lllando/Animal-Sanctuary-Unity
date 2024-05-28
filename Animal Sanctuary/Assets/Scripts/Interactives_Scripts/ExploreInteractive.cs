using UnityEngine;

public class ExploreInteractive : MonoBehaviour, Interactive
{
    [SerializeField] private float interactDistanceThreshold;

    public float InteractDistanceThreshold
    {
        get { return interactDistanceThreshold; }
    }

    public void Interact()
    {
        if (MeasurementUtility.IsNear(this.transform.position, GameManager.PlayerTransform.position, interactDistanceThreshold))
        {
            GameManager.InterfaceManager.DisplayConfirmCanvas();
        }
    }
}
