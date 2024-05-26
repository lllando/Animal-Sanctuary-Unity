using UnityEngine;

public class HabitatManager : MonoBehaviour
{
    [SerializeField] private Animal[] animalArray;

    [Header("Animal Controllers")]

    [SerializeField] private AnimalController animalControllerPrefab;

    private HabitatController _focusHabitat;

    private AnimalController _focusAnimal;

    private Item _potentialNewAnimal;

    public HabitatController FocusHabitat
    {
        set { _focusHabitat = value; }
        get { return _focusHabitat; }
    }

    public AnimalController FocusAnimal
    {
        set { _focusAnimal = value; }
        get { return _focusAnimal; }
    }

    public Item PotentialNewAnimal
    {
        get { return _potentialNewAnimal; }
    }

    public void AddAnimalToHabitat() //Via Inspector (Button)
    {
        GameManager.InterfaceManager.inventoryActive = false;

        if (_potentialNewAnimal != null && _focusHabitat != null)
        {
            Animal animal = null;

            foreach(Animal type in animalArray)
            {
                if(type.AnimalItem == _potentialNewAnimal)
                {
                    animal = type;
                    break;
                }
            }

            if(animal != null)
            {
                AnimalController controller = null;

                foreach (AnimalController ac in _focusHabitat.InactiveAnimalList)
                {
                    ac.gameObject.SetActive(true);
                    controller = ac;
                    break;
                }

                if (controller == null)
                {
                    controller = Instantiate(animalControllerPrefab, Vector3.zero, Quaternion.identity, _focusHabitat.transform);
                }

                controller.transform.position = _focusHabitat.transform.position;

                _focusHabitat.AnimalList.Add(controller);

                GameManager.InventoryManager.RemoveItem(_potentialNewAnimal, 1);

                controller.AssignAnimal(animal);

                _potentialNewAnimal = null;
            }
        }
    }

    public void AnimalSelectInventoryCheck(Item item)
    {
        if(_focusHabitat != null && item != null)
        {
            if (item.ItemTagList.Contains("Animal"))
            {
                _potentialNewAnimal = item;
                GameManager.InterfaceManager.DisplayAddHabitatAnimalConfirm(item);
            }
        }
    }

    public void RemoveAnimalFromHabitat() //Via Inspector
    {
        if(_focusAnimal != null && _focusHabitat != null)
        {
            GameManager.InventoryManager.AddItem(_focusAnimal.AssignedAnimal.AnimalItem, 1);

            _focusHabitat.AnimalList.Remove(_focusAnimal);
            _focusAnimal.gameObject.SetActive(false);

            GameManager.InterfaceManager.DisplayHabitatMenu(_focusHabitat);
        }
    }

    public void BuildHabitat()
    {
        _focusHabitat.IsBuilt = true;
        _focusHabitat = null;
    }

    public void RemoveFocusHabitat() //Via Inspector (Exit Button)
    {
        _focusHabitat = null;
        _focusAnimal = null;
    }
}
