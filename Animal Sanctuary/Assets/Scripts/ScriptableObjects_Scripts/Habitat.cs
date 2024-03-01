using UnityEngine;

[CreateAssetMenu(fileName = "Habitat", menuName = "ScriptableObjects/Habitat", order = 1)]
public class Habitat : ScriptableObject
{
    [SerializeField] private string habitatName;

    [SerializeField] private Item[] resourceRequirements;

    [SerializeField] private int[] resourceCountRequirement;

    public string HabitatName
    {
        get { return habitatName; }
    }

    public Item[] ResourceRequirements
    {
        get { return resourceRequirements; }
    }

    public int[] ResourceCountRequirements
    {
        get { return resourceCountRequirement; }
    }
}
