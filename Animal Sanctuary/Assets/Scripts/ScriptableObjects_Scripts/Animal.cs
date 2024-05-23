using UnityEngine;


[CreateAssetMenu(fileName = "Animal", menuName = "ScriptableObjects/Animal", order = 2)]
public class Animal : ScriptableObject
{
    [SerializeField] private string animalDescription;

    [SerializeField] private string animalSpecies;

    [SerializeField] private Item animalItem;

    [SerializeField] private Sprite animalIcon;

    [SerializeField] private Sprite animalImage;

    [SerializeField] private string[] animalFactArray;

    public string AnimalDescription
    {
        get { return animalDescription; }
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

    public Sprite AnimalImage
    {
        get { return animalImage; }
    }

    public string[] AnimalFactArray
    {
        get { return animalFactArray; }
    }
}