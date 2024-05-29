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

    private void Start()
    {
        StartCoroutine(ApplyIntervalStats());
    }

    private void Update()
    {
        if(_isBuilt)
        {
            foreach(AnimalController animal in animalList)
            {
                if (GameManager.HabitatManager.FocusAnimal == animal)
                {
                    animal.UpdateStatsRealTime();
                }
            }
        }
    }

    private IEnumerator ApplyIntervalStats()
    {
        yield return new WaitForSeconds(10);

        foreach (AnimalController animal in animalList)
        {
            if (GameManager.HabitatManager.FocusAnimal != animal)
            {
                animal.UpdateStatsIntervalTime();
            }
        }

        StartCoroutine(ApplyIntervalStats());
    }
}
