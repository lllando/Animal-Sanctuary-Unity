using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public enum Characters
    {
        Leo,
        Ella
    }

    public Characters currentCharacterSpeaking;

    public Dictionary<Characters, string> introductionDialogue = new Dictionary<Characters, string>()
    {
        {Characters.Leo, "Hello! How are you, mate! Welcome to Africa. How you doin’?"},
        {Characters.Ella, "Test"}

    };
}
