using UnityEngine;

public class HabitatManager : MonoBehaviour
{
    [SerializeField] private GameObject habitatPlacement;

    private Habitat _habitatToBuild;

    private void Update()
    {
        if(habitatPlacement.activeSelf)
        {
            habitatPlacement.transform.position = GameManager.MainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

            if(Input.GetMouseButtonDown(0))
            {
                BuildHabitat(_habitatToBuild);

                _habitatToBuild = null;
                habitatPlacement.SetActive(false);
            }    
        }
    }

    public void SelectHabitatToBuild() //Via Inspector (Button)
    {
        habitatPlacement.SetActive(true);
    }

    public void BuildHabitat(Habitat habitat)
    {
        for(int i = 0; i < habitat.ResourceRequirements.Length; i++)
        {
            if (!GameManager.InventoryManager.HasItem(habitat.ResourceRequirements[i], habitat.ResourceCountRequirements[i]))
            {
                Debug.Log("Item was not found: " + habitat.ResourceRequirements[i].name + "x" + habitat.ResourceCountRequirements[i].ToString());
                return;
            }
        }

        for(int i = 0; i < habitat.ResourceRequirements.Length; i++)
        {
            GameManager.InventoryManager.RemoveItem(habitat.ResourceRequirements[i], habitat.ResourceCountRequirements[i]);
        }

        _habitatToBuild = habitat;

        Debug.Log("Habitat Build!: " + habitat.name);
    }
}
