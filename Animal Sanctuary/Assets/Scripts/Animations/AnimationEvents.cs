using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private AudioClip nextDayInteractAudioClip;

    public void DisableInteract()
    {
        GameManager.GameManagerInstance.disableInteract = true;
    }

    public void EnableInteract()
    {
        AudioManager.Instance.PlayAudioUsingPrefab(gameObject.transform.position, nextDayInteractAudioClip);
        GameManager.GameManagerInstance.disableInteract = false;
    }
}
