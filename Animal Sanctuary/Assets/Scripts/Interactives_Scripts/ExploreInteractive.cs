using System;
using UnityEngine;

public class ExploreInteractive : MonoBehaviour, Interactive
{
    private AudioSource audioSource;
    
    [SerializeField] private AudioClip exploreAudioClip;
    
    [SerializeField] private float interactDistanceThreshold;

    public float InteractDistanceThreshold
    {
        get { return interactDistanceThreshold; }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void Interact()
    {
        if (MeasurementUtility.IsNear(this.transform.position, GameManager.PlayerTransform.position, interactDistanceThreshold))
        {
            AudioManager.Instance.PlayAudio(audioSource, exploreAudioClip);
            GameManager.InterfaceManager.DisplayConfirmCanvas();
        }
    }
}
