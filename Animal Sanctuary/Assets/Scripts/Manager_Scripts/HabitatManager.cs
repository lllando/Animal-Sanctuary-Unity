using UnityEngine;

public class HabitatManager : MonoBehaviour
{
    [SerializeField] private GameObject habitatPlacement;

    private Habitat _habitatToBuild;

    private void Update()
    {
        //if(habitatPlacement.activeSelf)
        //{
        //    habitatPlacement.transform.position = GameManager.MainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        //    if(Input.GetMouseButtonDown(0))
        //    {
        //        BuildHabitat(_habitatToBuild);

        //        _habitatToBuild = null;
        //        habitatPlacement.SetActive(false);
        //    }    
        //}
    }

    public void SelectHabitatToBuild() //Via Inspector (Button)
    {
        habitatPlacement.SetActive(true);
    }

    public void BuildHabitat(Habitat habitat)
    {
        
    }
}
