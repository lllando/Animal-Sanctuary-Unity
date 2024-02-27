using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    public GameObject SettingPanel;
    public GameObject PausePanel;
    void Start()
    {
        
    }

 
    public void Open()
    {
        PausePanel.SetActive(false);
        SettingPanel.SetActive(true);
        
    }
    
}
