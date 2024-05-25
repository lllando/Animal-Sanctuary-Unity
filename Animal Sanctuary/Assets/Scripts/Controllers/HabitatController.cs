using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatController : MonoBehaviour
{
    [SerializeField] private Habitat habitat;

    [SerializeField] private List<Animal> animalList = new List<Animal>();

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

    public List<Animal> AnimalList
    {
        get { return animalList; }
    }
}
