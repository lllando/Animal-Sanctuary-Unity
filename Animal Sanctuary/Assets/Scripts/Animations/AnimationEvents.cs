using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void DisableInteract()
    {
        GameManager.GameManagerInstance.disableInteract = true;
    }

    public void EnableInteract()
    {
        GameManager.GameManagerInstance.disableInteract = false;
    }
}
