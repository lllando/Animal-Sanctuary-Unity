using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [Header("Inventory")]

    [SerializeField] private Image[] inventoryIcon;

    [SerializeField] private TextMeshProUGUI[] inventoryCountText;

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
            if (item.Item != null)
            {
                inventoryIcon[index].gameObject.SetActive(true);
                inventoryIcon[index].sprite = item.Item.ItemIcon;
                inventoryCountText[index].text = item.StackSize.ToString("00");
            }
            else
            {
                inventoryIcon[index].sprite = null;
                inventoryIcon[index].gameObject.SetActive(false);
                inventoryCountText[index].text = "00";
            }

            index++;
        }
    }

    public void DisplayAnimalScreen(Animal animal) //Via Inspector (Button)
    {
        animalScreen.SetActive(true);
        animalNameText.text = animal.AnimalSpecies + " was added to your inventory!";
        animalIconImage.sprite = animal.AnimalIcon;
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
    }
}
