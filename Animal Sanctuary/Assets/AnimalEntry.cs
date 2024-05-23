using UnityEngine;
using UnityEngine.UI;

public class AnimalEntry : MonoBehaviour
{
    [SerializeField] private Image animalIcon;

    private Animal _assignedAnimal;

    private Button _thisButton;

    public Animal AssignedAnimal
    {
        get { return _assignedAnimal; }
        set { _assignedAnimal = value; }
    }

    public Image AnimalIcon
    {
        get { return animalIcon; }
    }

    private void Awake()
    {
        _thisButton = this.GetComponent<Button>();
        _thisButton.onClick.AddListener(DisplayAnimalDetails);
    }

    private void DisplayAnimalDetails()
    {
        GameManager.InterfaceManager.DisplayAnimalInformation(_assignedAnimal);
    }
}
