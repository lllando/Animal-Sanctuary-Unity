using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private GameObject audioPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudio(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);   
    }

    public void PlayAudioUsingPrefab(Vector3 position, AudioClip audioClip, bool pitchRandomness = false)
    {

        GameObject audio = Instantiate(audioPrefab, position, Quaternion.identity);
        AudioSource prefabAudioSource = audio.GetComponent<AudioSource>();
        
        if (pitchRandomness)
        {
            prefabAudioSource.pitch = 1f + Random.Range (-0.2f, 0.2f);
        }
        
        prefabAudioSource.PlayOneShot(audioClip);
    }
}
