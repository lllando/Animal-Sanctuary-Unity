using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static InventoryManager InventoryManager;
    public static InterfaceManager InterfaceManager;

    private void Awake()
    {
        InventoryManager = this.GetComponent<InventoryManager>();
        InterfaceManager = this.GetComponent<InterfaceManager>();
    }
}
