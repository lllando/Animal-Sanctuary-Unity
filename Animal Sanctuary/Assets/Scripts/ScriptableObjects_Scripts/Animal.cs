using UnityEngine;


[CreateAssetMenu(fileName = "Animal", menuName = "ScriptableObjects/Animal", order = 2)]
public class Animal : ScriptableObject
{
    [SerializeField] private string animalName;

    [SerializeField] private string animalSpecies;

    [SerializeField] private Item animalItem;

    [SerializeField] private Sprite animalIcon;

    public string AnimalName
    {
        get { return animalName; }
    }

    public string AnimalSpecies
    {
        get { return animalSpecies; }
    }

    public Item AnimalItem
    {
        get { return animalItem; }
    }

    public Sprite AnimalIcon
    {
        get { return animalIcon; }
    }
}