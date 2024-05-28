using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame() //Via Inspector (Button)
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() //Via Inspector (Button)
    {
        Application.Quit();
    }
}
