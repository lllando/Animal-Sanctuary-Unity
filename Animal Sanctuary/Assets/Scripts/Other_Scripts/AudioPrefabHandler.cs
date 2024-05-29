using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPrefabHandler : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Debug.Log("Deleted audio prefab");
            Destroy(this.gameObject);
        }
    }
}
