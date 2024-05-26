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
                if(!GameManager.InterfaceManager.inventoryActive)
                {
                    GameManager.InterfaceManager.DisplayHabitatMenu(_habitatController);
                    GameManager.HabitatManager.FocusHabitat = _habitatController;
                }
            }
            else
            {
                GameManager.InterfaceManager.DisplayBuildHabitatScreen(_habitatController);
                GameManager.HabitatManager.FocusHabitat = _habitatController;
                Debug.Log("Habitat Interact!");
            }
        }
    }
}
