using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueInteractive : MonoBehaviour, Interactive
{
    [Header("Dialogue")]

    [SerializeField] private UnityEvent[] dialogueEndEvent;

    [SerializeField] private bool isActive;

    [SerializeField] private GameObject dialogueCamera;

    [SerializeField] private string dialogueTextPath;

    public UnityEvent[] DialogueEndEvent
    {
        get { return dialogueEndEvent; }
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = false; }
    }

    public GameObject DialogueCamera
    {
        get { return dialogueCamera; }
        set { dialogueCamera = value; }
    }

    public string DialogueTextPath
    {
        get { return dialogueTextPath; }
    }

    public void Interact()
    {

    }
}
