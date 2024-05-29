using System.Collections.Generic;
using UnityEngine;

public class HabitatManager : MonoBehaviour
{
    [SerializeField] private Animal[] animalArray;

    [SerializeField] private string[] randomNames;

    [Header("Animal Controllers")]

    [SerializeField] private AnimalController animalControllerPrefab;

    private HabitatController _focusHabitat;

    private AnimalController _focusAnimal;

    private Item _potentialNewAnimal;

    private List<HabitatController> _habitatList = new List<HabitatController>();

    public List<HabitatController> HabitatList
    {
        get { return _habitatList; }
    }

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

                int randX = Random.Range(-2, 2);
                int randY = Random.Range(-2, 2);

                controller.transform.position = _focusHabitat.transform.position + new Vector3(randX, randY, 0);

                _focusHabitat.AnimalList.Add(controller);

                GameManager.InventoryManager.RemoveItem(_potentialNewAnimal, 1);

                controller.AnimalName = randomNames[Random.Range(0, randomNames.Length)];

                controller.AssignAnimal(animal);

                _potentialNewAnimal = null;
            }
        }
    }

    public void AnimalSelectInventoryCheck(Item item)
    {
        if(_focusHabitat != null && item != null)
        {
            if(_focusHabitat.AnimalList.Count > 0)
            {
                if (GameManager.HabitatManager.GetAnimalFromItem(item) != _focusHabitat.AnimalList[0].AssignedAnimal)
                {
                    return;
                }
            }

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
        _habitatList.Add(_focusHabitat);
        _focusHabitat = null;
    }

    public void RemoveFocusHabitat() //Via Inspector (Exit Button)
    {
        _focusHabitat = null;
        _focusAnimal = null;
    }

    public void UpdateAnimalStatistic(int index, Item item)
    {
        _focusAnimal.StatsArray[index] += item.ItemUseIncrease;

        if (_focusAnimal.StatsArray[index] > 100)
        {
            _focusAnimal.StatsArray[index] = 100;
        }

        GameManager.InventoryManager.RemoveItem(item, 1);
    }

    public void UpdateAnimalName()
    {
        if(_focusAnimal != null && _focusHabitat != null)
        {
            _focusAnimal.AnimalName = GameManager.InterfaceManager.animalNameInputField.text;
            GameManager.InterfaceManager.DisplayHabitatMenu(_focusHabitat);
            GameManager.InterfaceManager.UpdateAnimalDisplay();
            Debug.Log("Animal Name Updated!");
        }
    }

    public Item GetAnimalFromItem(Item item)
    {
        foreach(Animal animal in animalArray)
        {
            if(animal.AnimalItem == item)
            {
                return item;
            }
        }

        return null;
    }

    public void UpdateAnimalStats()
    {
        foreach(HabitatController habitat in _habitatList)
        {
            habitat.UpdateAnimalStats();
        }
    }
}
