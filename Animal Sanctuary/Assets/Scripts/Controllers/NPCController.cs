using UnityEngine;

public class NPCController : MonoBehaviour, Interactive
{
    [SerializeField] private DialogueInteractive[] dialogueInteractive;

    [SerializeField] private float interactDistanceThreshold;
    
    private int _dialogueIndex;

    public int DialogueIndex
    {
        get { return _dialogueIndex; }
        set { _dialogueIndex = value; }
    }

    public float InteractDistanceThreshold
    {
        get { return interactDistanceThreshold; }
    }

    public void Interact()
    {
        if(dialogueInteractive != null && MeasurementUtility.IsNear(this.transform.position, GameManager.PlayerTransform.position, interactDistanceThreshold) && _dialogueIndex < dialogueInteractive.Length)
        {
            GameManager.DialogueManager.StartDialogue(dialogueInteractive[_dialogueIndex]);
            Debug.Log("woo hoooo!");
        }

        Debug.Log("Oh nooo");
    }
}
