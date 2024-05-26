using Unity.Android.Gradle;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    private string _animalName = "Bob";

    private Animal _assignedAnimal;

    private float[] _statsArray = new float[3];

    public string AnimalName
    {
        get { return _animalName; }
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

    public void UpdateStatsRealTime()
    {
        for(int i = 0; i < _statsArray.Length; i++)
        {
            _statsArray[i] -= Time.deltaTime * 0.01f;
        }
    }

    public void UpdateStatsIntervalTime()
    {
        for (int i = 0; i < _statsArray.Length; i++)
        {
            _statsArray[i] -= Time.deltaTime * 0.1f;
        }
    }
}
