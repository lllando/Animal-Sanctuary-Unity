using TMPro;
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

    public void UpdateInventory(InventoryItem[] inventory)
    {
        int index = 0;

        foreach(InventoryItem item in inventory)
        {
            if (item.Item != null)
            {
                inventoryIcon[index].sprite = item.Item.ItemIcon;
                inventoryCountText[index].text = item.StackSize.ToString();
            }
            else
            {
                inventoryIcon[index].sprite = null;
                inventoryCountText[index].text = "0";
            }

            index++;
        }
    }

    public void DisplayAnimalScreen(Animal animal) //Via Inspector (Button)
    {
        animalScreen.SetActive(true);
        animalNameText.text = animal.AnimalName + " the " + animal.AnimalSpecies + " was added to your inventory!";
        animalIconImage.sprite = animal.AnimalIcon;
        GameManager.InventoryManager.AddItem(animal.AnimalItem, 1);
    }
}
