using UnityEngine;

public class NPCController : MonoBehaviour, Interactive
{
    [SerializeField] private DialogueInteractive dialogueInteractive;

    [SerializeField] private float interactDistanceThreshold;

    public float InteractDistanceThreshold
    {
        get { return interactDistanceThreshold; }
    }

    public void Interact()
    {
        if(dialogueInteractive != null && MeasurementUtility.IsNear(this.transform.position, GameManager.PlayerTransform.position, interactDistanceThreshold))
        {
            GameManager.DialogueManager.StartDialogue(dialogueInteractive);
            Debug.Log("woo hoooo!");
        }

        Debug.Log("Oh nooo");
    }
}
