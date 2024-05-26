using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HabitatAnimalEntry : MonoBehaviour
{
    private Image _animalIcon;

    private TextMeshProUGUI _animalNameText;

    private Button _thisButton;

    private AnimalController _assignedAnimal;

    private void Awake()
    {
        _animalIcon = this.transform.GetChild(0).GetComponent<Image>();
        _animalNameText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _thisButton = this.GetComponent<Button>();

        _thisButton.onClick.AddListener(InspectAnimal);
    }

    public void AssignHabitatAnimal(AnimalController animalController)
    {
        Animal animal = animalController.AssignedAnimal;
        _animalIcon.sprite = animal.AnimalIcon;
        _animalNameText.text = animal.name;
        _assignedAnimal = animalController;
    }

    private void InspectAnimal()
    {
        GameManager.HabitatManager.FocusAnimal = _assignedAnimal;
        GameManager.InterfaceManager.UpdateAnimalDisplay();
    }
}
