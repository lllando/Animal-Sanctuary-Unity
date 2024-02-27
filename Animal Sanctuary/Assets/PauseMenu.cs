using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject SettingMenu;
    
    void Start()
    {
        
    }

    
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        SettingMenu.SetActive(false);
        Time.timeScale = 1;
    }


    public void EndGame()
    {
        Application.Quit();
        Debug.Log("EndGame");
    }
}
