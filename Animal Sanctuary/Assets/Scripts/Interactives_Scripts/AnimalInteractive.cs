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
        GameManager.InterfaceManager.DisplayAnimalScreen(animal);
    }
}
