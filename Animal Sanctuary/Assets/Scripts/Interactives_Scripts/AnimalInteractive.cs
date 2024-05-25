using UnityEngine;

public class AnimalInteractive : MonoBehaviour, Interactive
{
    [SerializeField] private Animal animal;

    [SerializeField] private float interactDistanceThreshold;

    public float InteractDistanceThreshold
    {
        get { return interactDistanceThreshold; }
    }

    public void Interact()
    {
        if(MeasurementUtility.IsNear(this.transform.position, GameManager.PlayerTransform.position, interactDistanceThreshold))
        {
            GameManager.InterfaceManager.DisplayAnimalScreen(animal);
            Debug.Log("Animal Interact!");
        }
    }
}
