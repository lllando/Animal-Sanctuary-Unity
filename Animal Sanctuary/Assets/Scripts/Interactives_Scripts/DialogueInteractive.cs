using UnityEngine;
using UnityEngine.Events;

public class DialogueInteractive : MonoBehaviour
{
    [Header("Dialogue")]

    [SerializeField] private UnityEvent[] dialogueEndEvent;

    [SerializeField] private string dialogueTextPath;

    public UnityEvent[] DialogueEndEvent
    {
        get { return dialogueEndEvent; }
    }

    public string DialogueTextPath
    {
        get { return dialogueTextPath; }
    }
}
