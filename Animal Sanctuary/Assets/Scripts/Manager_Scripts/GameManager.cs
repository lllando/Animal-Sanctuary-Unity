using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool disableInteract;

    public static InventoryManager InventoryManager;
    public static InterfaceManager InterfaceManager;
    public static Camera MainCamera;

    public static DialogueManager DialogueManager;
    public static ResourceManager ResourceManager;

    public static HabitatManager HabitatManager;

    public static Transform PlayerTransform;

    public bool DisableInteract
    {
        get { return disableInteract; }
        set { disableInteract = value; }
    }
     
    private void Awake()
    {
        InventoryManager = this.GetComponent<InventoryManager>();
        InterfaceManager = this.GetComponent<InterfaceManager>();
        DialogueManager = this.GetComponent<DialogueManager>();
        ResourceManager = this.GetComponent<ResourceManager>();
        HabitatManager = this.GetComponent<HabitatManager>();

        PlayerTransform = GameObject.Find("Player").transform;

        MainCamera = Camera.main;
    }

    private void Update()
    {
        if(!DisableInteract)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerInteractRaycast();
                Debug.Log("Interact");
            }
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
            }

            Debug.Log("Mouse detected an object");
        }
    }
}
