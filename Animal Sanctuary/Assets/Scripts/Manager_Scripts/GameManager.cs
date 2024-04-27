using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static InventoryManager InventoryManager;
    public static InterfaceManager InterfaceManager;
    public static Camera MainCamera;
     
    private void Awake()
    {
        InventoryManager = this.GetComponent<InventoryManager>();
        InterfaceManager = this.GetComponent<InterfaceManager>();

        MainCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayerInteractRaycast();
            Debug.Log("Interact");
        }
    }

    private void PlayerInteractRaycast()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if(hit.collider != null)
        {
            if(hit.collider.TryGetComponent(out Interactive component))
            {
                component.Interact();
                Destroy(hit.collider.gameObject);
            }

            Debug.Log("Mouse detected an object");
        }
    }
}
