using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool disableInteract;

    public static GameManager GameManagerInstance;

    public static InventoryManager InventoryManager;
    public static InterfaceManager InterfaceManager;
    public static Camera MainCamera;

    public static DialogueManager DialogueManager;
    public static ResourceManager ResourceManager;

    public static HabitatManager HabitatManager;

    public static PlayerController PlayerController;
    public static Transform PlayerTransform;

    public static SaveManager SaveManager;

    public static ShopManager ShopManager;

    public static int DayCount;

    [Header("Day Transition")]

    [SerializeField] private NPCController quizNPC;

    [SerializeField] private Animator dayTransitionAnimator;

    public bool DisableInteract
    {
        get { return disableInteract; }
        set { disableInteract = value; }
    }
     
    private void Awake()
    {
        GameManagerInstance = this;

        InventoryManager = this.GetComponent<InventoryManager>();
        InterfaceManager = this.GetComponent<InterfaceManager>();
        DialogueManager = this.GetComponent<DialogueManager>();
        ResourceManager = this.GetComponent<ResourceManager>();
        HabitatManager = this.GetComponent<HabitatManager>();
        SaveManager = this.GetComponent<SaveManager>();

        this.TryGetComponent(out ShopManager shop);
        ShopManager = shop;

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

    public void IncreaseDay() //Via Inspector (Button)
    {
        DayCount++;
        SaveManager.SaveGame();
        InterfaceManager.UpdateDayText(DayCount);

        HabitatManager.UpdateAnimalStats();

        quizNPC.DialogueIndex = 1;

        dayTransitionAnimator.SetTrigger("transition");
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
