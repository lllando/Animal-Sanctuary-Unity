using UnityEngine;

public class NextDayInteractive : MonoBehaviour, Interactive
{
    [SerializeField] private float interactDistanceThreshold;

    [SerializeField] private AudioClip nextDayInteractAudioClip;

    public float InteractDistanceThreshold
    {
        get { return interactDistanceThreshold; }
    }

    public void Interact()
    {
        if (MeasurementUtility.IsNear(this.transform.position, GameManager.PlayerTransform.position, interactDistanceThreshold))
        {
            GameManager.InterfaceManager.DisplayEndDayConfirm();
            Debug.Log("Animal Interact!");
            
            AudioManager.Instance.PlayAudioUsingPrefab(gameObject.transform.position, nextDayInteractAudioClip);

        }
    }
}
