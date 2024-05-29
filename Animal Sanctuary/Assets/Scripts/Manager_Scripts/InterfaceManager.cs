using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public enum ItemInteraction { None, Animal, Food, Water, Medicine, Sell }

    public static ItemInteraction ActiveItemInteraction;

    public bool inventoryActive;

    public TMP_InputField animalNameInputField;

    [Header("Inventory")]

    [SerializeField] private InventorySlot[] inventorySlotArray;

    [Header("Animal Screen")]

    [SerializeField] private GameObject animalScreen;

    [SerializeField] private TextMeshProUGUI animalNameText;

    [SerializeField] private Image animalIconImage;

    [Header("Animal Catalogue")]

    [SerializeField] private Animal[] animalArray;

    [SerializeField] private AnimalEntry[] animalEntry;

    [Header("Animal Display")]

    [SerializeField] private GameObject animalSelectObj;

    [SerializeField] private GameObject animalDisplayObj;

    [SerializeField] private Image animalImage;

    [SerializeField] private TextMeshProUGUI animalNameDisplayText;

    [SerializeField] private TextMeshProUGUI animalDescriptionText;

    [SerializeField] private TextMeshProUGUI[] animalFactTextArray;

    [Header("Habitat Build Screen")]

    [SerializeField] private GameObject habitatBuildMenuObj;

    [SerializeField] private TextMeshProUGUI habitatNameText;

    [SerializeField] private TextMeshProUGUI[] resourceCountTextArray;

    [SerializeField] private Image[] resourceIconArray;

    [SerializeField] private Button buildHabitatButton;

    [Header("Habitat Menu")]

    [SerializeField] private GameObject habitatMenuObj;

    [SerializeField] private GameObject animalDetailsObj;

    [Header("Animal Screen")]

    [SerializeField] private TextMeshProUGUI animalInfoText;

    [SerializeField] private Slider[] animalStatsArray;

    [SerializeField] private HabitatAnimalEntry animalHabitatEntryPrefab;

    [SerializeField] private Transform animalHabitatEntryTransform;

    [SerializeField] private GameObject confirmAddAnimalObj;

    [SerializeField] private Image animalIcon;

    [Header("Confirm")]

    [SerializeField] private GameObject confirmCanvas;

    private List<HabitatAnimalEntry> _animalHabitatEntryList = new List<HabitatAnimalEntry>();

    public bool InventoryActive
    {
        get { return inventoryActive; }
        set { inventoryActive = value; }
    }

    private void Awake()
    {
        foreach(InventorySlot slot in inventorySlotArray)
        {
            slot.Initialise();
        }
    }

    private void Update()
    {
        if(animalDetailsObj.activeSelf && GameManager.HabitatManager.FocusAnimal != null)
        {
            for (int i = 0; i < animalStatsArray.Length; i++)
            {
                animalStatsArray[i].maxValue = 100;
                animalStatsArray[i].value = GameManager.HabitatManager.FocusAnimal.StatsArray[i];
            }
        }
    }

    public void DisplayAnimalSelectMenu()
    {
        for(int i = 0; i < animalArray.Length; i++)
        {
            animalEntry[i].AssignedAnimal = animalArray[i];
            animalEntry[i].AnimalIcon.sprite = animalArray[i].AnimalIcon;
        }
    }

    public void DisplayAnimalInformation(Animal animal)
    {
        animalSelectObj.SetActive(false);
        animalDisplayObj.SetActive(true);

        animalNameDisplayText.text = animal.name;
        animalDescriptionText.text = animal.AnimalDescription;

        for (int i = 0; i < animal.AnimalFactArray.Length; i++)
        {
            animalFactTextArray[i].text = animal.AnimalFactArray[i];
        }

        animalImage.sprite = animal.AnimalImage;
        animalImage.SetNativeSize();
        GameManager.InventoryManager.AddItem(animal.AnimalItem, 1);
    }

    public void UpdateInventory()
    {
        InventoryItem[] inventory = GameManager.InventoryManager.Inventory;

        int index = 0;

        foreach(InventoryItem item in inventory)
        {
            inventorySlotArray[index].AssignItem(item.Item, item.StackSize);
            index++;
        }
    }

    public void DisplayAnimalScreen(Animal animal) //Via Inspector (Button)
    {
        animalScreen.SetActive(true);
        animalNameText.text = animal.AnimalSpecies + " was added to your inventory!";
        animalIconImage.sprite = animal.AnimalIcon;
        GameManager.InventoryManager.AddItem(animal.AnimalItem, 1);
    }

    public void DisplayBuildHabitatScreen(HabitatController habitatController)
    {
        habitatBuildMenuObj.SetActive(true);

        Habitat habitat = habitatController.Habitat;

        habitatNameText.text = habitat.HabitatName;

        int hasResourceCount = 0;

        for(int i = 0; i < habitat.ResourceRequirements.Length; i++)
        {
            int currentCount = GameManager.InventoryManager.GetItemCount(habitat.ResourceRequirements[i]);

            resourceCountTextArray[i].text = currentCount.ToString() + "/" + habitat.ResourceCountRequirements[i].ToString();
            resourceIconArray[i].sprite = habitat.ResourceRequirements[i].ItemIcon;

            if (GameManager.InventoryManager.HasItem(habitat.ResourceRequirements[i], habitat.ResourceCountRequirements[i]))
            {
                hasResourceCount++;
            }
        }

        buildHabitatButton.interactable = hasResourceCount == 3;
    }

    public void DisplayHabitatMenu(HabitatController habitatController)
    {
        habitatMenuObj.SetActive(true);
        animalDetailsObj.SetActive(habitatController.AnimalList.Count > 0);

        foreach(HabitatAnimalEntry entry in _animalHabitatEntryList)
        {
            entry.gameObject.SetActive(false);
        }

        if(habitatController.AnimalList.Count > 0)
        {
            GameManager.HabitatManager.FocusAnimal = habitatController.AnimalList[0];
            UpdateAnimalDisplay();

            foreach (AnimalController controller in habitatController.AnimalList)
            {
                HabitatAnimalEntry habitatAnimalEntry = null;

                foreach (HabitatAnimalEntry entry in _animalHabitatEntryList)
                {
                    if (!entry.gameObject.activeSelf)
                    {
                        habitatAnimalEntry = entry;
                        habitatAnimalEntry.gameObject.SetActive(true);
                        break;
                    }
                }

                if (habitatAnimalEntry == null)
                {
                    habitatAnimalEntry = Instantiate(animalHabitatEntryPrefab, Vector3.zero, Quaternion.identity, animalHabitatEntryTransform);
                    _animalHabitatEntryList.Add(habitatAnimalEntry);
                }

                habitatAnimalEntry.AssignHabitatAnimal(controller);
            }
        }
    }

    public void HideHabitatMenu() //Via Inspector (Button)
    {
        animalDetailsObj.SetActive(false);
        habitatMenuObj.SetActive(false);
        GameManager.HabitatManager.FocusHabitat = null;
    }

    public void UpdateAnimalDisplay()
    {
        animalDetailsObj.SetActive(true);

        for (int i = 0; i < animalStatsArray.Length; i++)
        {
            animalStatsArray[i].maxValue = 100;
            animalStatsArray[i].value = GameManager.HabitatManager.FocusAnimal.StatsArray[i];
        }

        animalNameInputField.text = GameManager.HabitatManager.FocusAnimal.AnimalName;

        Animal animal = GameManager.HabitatManager.FocusAnimal.AssignedAnimal;

        animalInfoText.text = "Species: " + animal.AnimalSpecies + "\nOrigin: " + animal.AnimalOrigin + "\nSurvival Status: " + animal.SurvivalStatus;
    }

    public void DisplayAddHabitatAnimalConfirm(Item item)
    {
        confirmAddAnimalObj.SetActive(true);
        animalIcon.sprite = item.ItemIcon;
    }

    public void DisplayConfirmCanvas()
    {
        confirmCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void SetInventoryInteraction(int index) //Via Inspector (Button)
    {
        ActiveItemInteraction = (ItemInteraction)index;
        inventoryActive = index != 0;
    }

    [Header("Shop Menu")]

    [SerializeField] private GameObject shopCanvasObj;

    [SerializeField] private GameObject buyPanelObj;

    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] private TextMeshProUGUI itemNameText;

    [SerializeField] private Image itemIcon;

    [SerializeField] private Slider itemSlider;

    [SerializeField] private TextMeshProUGUI itemCountText;

    [SerializeField] private Button buyButton;

    [SerializeField] private TraderSlot[] traderSlotArray;

    [SerializeField] private TextMeshProUGUI costText;

    [SerializeField] private TextMeshProUGUI totalCoinsText;

    public int ItemCount
    {
        get { return (int)itemSlider.value; }
    }

    public void DisplayShop(InventoryItem[] shopInventory)
    {
        shopCanvasObj.SetActive(true);
        coinText.text = GameManager.InventoryManager.GetItemCount(GameManager.InventoryManager.coinItem).ToString();

        //Update Shop Inventory

        int index = 0;

        foreach(TraderSlot slot in traderSlotArray)
        {
            slot.AssignItem(shopInventory[index].Item, shopInventory[index].StackSize);
            index++;
        }
    }

    public void DisplayBuyItem(Item item, int stackSize)
    {
        buyPanelObj.SetActive(true);

        itemNameText.text = item.ItemName;
        itemIcon.sprite = item.ItemIcon;

        itemSlider.maxValue = stackSize;
        itemSlider.value = 1;

        itemCountText.text = "1";

        costText.text = "COST: " + item.ItemCost.ToString();
        totalCoinsText.text = "COINS: " + GameManager.InventoryManager.Coins.ToString();

        BuyItemSliderUpdate();
    }

    public void BuyItemSliderUpdate() //Via Inspector (Slider)
    {
        Item item = GameManager.ShopManager.focusItem;
        int individualCost = item.ItemCost;
        float totalCost = individualCost * itemSlider.value;

        costText.text = "COST: " + (item.ItemCost * itemSlider.value).ToString();
        totalCoinsText.text = "COINS: " + GameManager.InventoryManager.Coins.ToString();

        itemCountText.text = itemSlider.value.ToString();

        buyButton.interactable = totalCost <= GameManager.InventoryManager.Coins;
    }

    [Header("Day Cycle")]

    [SerializeField] private TextMeshProUGUI dayCountText;

    [SerializeField] private GameObject confirmDayObj;

    public void UpdateDayText(int day)
    {
        dayCountText.text = "Day " + day.ToString();
    }

    public void DisplayEndDayConfirm()
    {
        GameManager.GameManagerInstance.disableInteract = true;
        confirmDayObj.SetActive(true);
    }
}
