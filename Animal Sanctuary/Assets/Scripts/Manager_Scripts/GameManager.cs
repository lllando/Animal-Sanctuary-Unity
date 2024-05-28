using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool disableInteract;

    public static InventoryManager InventoryManager;
    public static InterfaceManager InterfaceManager;
    public static Camera MainCamera;

    public static DialogueManager DialogueManager;
    public static ResourceManager ResourceManager;

    public static HabitatManager HabitatManager;

    public static PlayerController PlayerController;
    public static Transform PlayerTransform;

    public static SaveManager SaveManager;

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
        SaveManager = this.GetComponent<SaveManager>();

        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        PlayerTransform = PlayerController.transform;

        MainCamera = Camera.main;
    }

    private void Start()
    {
        SaveManager.LoadGame();
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

    public void LoadScene(int index)
    {
        SaveManager.SaveGame();
        SceneManager.LoadScene(index);
    }

    public void ReturnTimeScale()
    {
        Time.timeScale = 1;
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
