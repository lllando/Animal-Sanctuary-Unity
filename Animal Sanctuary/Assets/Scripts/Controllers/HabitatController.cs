using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatController : MonoBehaviour
{
    [SerializeField] private Habitat habitat;

    [SerializeField] private List<AnimalController> animalList = new List<AnimalController>();

    private List<AnimalController> _inactiveAnimalList = new List<AnimalController>();

    private bool _isBuilt;

    public Habitat Habitat
    {
        get { return habitat; }
    }

    public bool IsBuilt
    {
        get { return _isBuilt; }
        set { _isBuilt = value; }
    }

    public List<AnimalController> AnimalList
    {
        get { return animalList; }
    }

    public List<AnimalController> InactiveAnimalList
    {
        get { return _inactiveAnimalList; }
    }

    public void UpdateAnimalStats()
    {
        foreach (AnimalController animal in animalList)
        {
            animal.UpdateStatsIntervalTime();
        }
    }
}
