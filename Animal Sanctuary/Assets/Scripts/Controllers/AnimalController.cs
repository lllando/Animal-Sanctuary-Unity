using UnityEngine;

public class AnimalController : MonoBehaviour
{
    private string _animalName = "Bob";

    private Animal _assignedAnimal;

    private float[] _statsArray = new float[3];

    public string AnimalName
    {
        get { return _animalName; }
        set { _animalName = value; }
    }

    public Animal AssignedAnimal
    {
        get { return _assignedAnimal; }
        set { _assignedAnimal = value; }
    }

    public float[] StatsArray
    {
        get { return _statsArray; }
    }

    public void AssignAnimal(Animal animal)
    {
        _assignedAnimal = animal;
        this.transform.localScale = animal.SpriteScale;
        this.GetComponent<SpriteRenderer>().sprite = animal.AnimalIcon;

        for (int i = 0; i < _statsArray.Length; i++)
        {
            _statsArray[i] = 100;
        }
    }

    public void UpdateStatsIntervalTime()
    {
        _statsArray[0] -= _assignedAnimal.FoodDayDecrease;
        _statsArray[1] -= _assignedAnimal.ThirstDayDecrease;
        _statsArray[2] -= _assignedAnimal.HealthDayDecrease;
    }
}
