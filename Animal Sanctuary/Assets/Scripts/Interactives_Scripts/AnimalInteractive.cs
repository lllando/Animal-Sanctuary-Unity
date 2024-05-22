using UnityEngine;

public class AnimalInteractive : MonoBehaviour, Interactive
{
    [SerializeField] private Animal animal;

    public void Interact()
    {
        GameManager.InterfaceManager.DisplayAnimalScreen(animal);
    }
}
