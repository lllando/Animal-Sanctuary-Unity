using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public InventoryItem[] inventory = new InventoryItem[12];

    public PlayerData playerData;

    private void Awake()
    {
        playerData = new PlayerData();

        playerData.itemIDArray = new int[12];
        playerData.itemStackArray = new int[12];

        inventory = new InventoryItem[12];

        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = new InventoryItem(null, 0);
        }
    }

    public void SaveGame()
    {
        int[] itemIDArray = new int[12];
        int[] itemStackArray = new int[12];

        for(int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].Item == null)
            {
                itemIDArray[i] = 0;
                itemStackArray[i] = 0;
            }
            else
            {
                itemIDArray[i] = inventory[i].Item.ItemID;
                itemStackArray[i] = inventory[i].StackSize;
            }
        }

        playerData.itemIDArray = itemIDArray;
        playerData.itemStackArray = itemStackArray;

        playerData.dayNum = GameManager.DayCount;

        string json = JsonUtility.ToJson(playerData);
        string filePath = Application.streamingAssetsPath + "/Saves/save.txt";
        System.IO.File.WriteAllText(filePath, json);
    }

    public void LoadGame()
    {
        string filePath = Application.streamingAssetsPath + "/Saves/save.txt";

        if (!System.IO.File.Exists(filePath))
        {
            SaveGame();
        }

        string jsonFile = System.IO.File.ReadAllText(filePath);
        playerData = JsonUtility.FromJson<PlayerData>(jsonFile);

        for(int i = 0; i < playerData.itemIDArray.Length; i++)
        {
            inventory[i].Item = GetItemViaID(playerData.itemIDArray[i]);
            inventory[i].StackSize = playerData.itemStackArray[i];
        }

        GameManager.DayCount = playerData.dayNum;
        GameManager.InterfaceManager.UpdateDayText(playerData.dayNum);
    }

    public Item GetItemViaID(int id)
    {
        foreach(Item item in GameManager.InventoryManager.itemArray)
        {
            if(item.ItemID == id)
            {
                return item;
            }
        }

        return null;
    }
}

[System.Serializable]
public class PlayerData
{
    public int[] itemIDArray;

    public int[] itemStackArray;

    public int dayNum;
}
