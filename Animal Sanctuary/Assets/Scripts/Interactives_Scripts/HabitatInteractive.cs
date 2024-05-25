using UnityEngine;

public class HabitatInteractive : MonoBehaviour, Interactive
{
    [SerializeField] private float interactDistanceThreshold;

    private HabitatController _habitatController;

    public float InteractDistanceThreshold
    {
        get { return interactDistanceThreshold; }
    }

    private void Awake()
    {
        _habitatController = this.GetComponent<HabitatController>();
    }

    public void Interact()
    {
        if (MeasurementUtility.IsNear(this.transform.position, GameManager.PlayerTransform.position, interactDistanceThreshold))
        {
            if(_habitatController.IsBuilt)
            {
                GameManager.InterfaceManager.DisplayHabitatMenu(_habitatController);
            }
            else
            {
                GameManager.InterfaceManager.DisplayBuildHabitatScreen(_habitatController);
                Debug.Log("Habitat Interact!");
            }
        }
    }
}
