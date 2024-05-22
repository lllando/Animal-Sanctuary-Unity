using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public bool dialogueActive;

    [Header("Option Display")]

    [SerializeField] private GameObject optionsInterface;

    [SerializeField] private TextMeshProUGUI[] optionTextArray;

    [SerializeField] private GameObject[] optionTextObjectArray;

    [Header("User Dialogue Display")]

    [SerializeField] private GameObject dialogueDisplay;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private Button continueDialogueButton;

    private DialogueInteractive _dialogueInteractive;

    private string[] _dialogueLines = new string[0];

    private string[] _optionArray;

    private int _dialogueIndex;

    public void StartDialogue(DialogueInteractive dialogueInteractive)
    {
        if (_dialogueLines.Length > 0)
        {
            return;
        }

        dialogueActive = true;

        _dialogueInteractive = dialogueInteractive;

        continueDialogueButton.gameObject.SetActive(true);

        string readFromFilePath = Application.streamingAssetsPath + "/TextAssets/Dialogue/" + _dialogueInteractive.DialogueTextPath;
        _dialogueLines = System.IO.File.ReadAllLines(readFromFilePath);

        NextDialogue();

        Debug.Log("Dialogue Started!");
    }

    public void NextDialogue()
    {
        Debug.Log("Displaying next dialogue!");

        string dialogueLine = _dialogueLines[_dialogueIndex];

        if (_dialogueIndex >= _dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            if (_dialogueLines[_dialogueIndex][0] == '*') //Is an option, so display all objects
            {
                _optionArray = new string[3];
                int optionIndex = 0;

                _optionArray[0] = "";
                _optionArray[1] = "";
                _optionArray[2] = "";

                for (int i = 1; i < dialogueLine.Length; i++)
                {
                    if (dialogueLine[i] == '*') // '*' == Option
                    {
                        optionIndex++;
                    }
                    else
                    {
                        _optionArray[optionIndex] += dialogueLine[i];
                    }
                }

                DisplayDialogueOptions(_optionArray);
            }
            else
            {
                if (_dialogueLines[_dialogueIndex].Length > 0) //This string indicates that the dialogue should end
                {
                    if (_dialogueLines[_dialogueIndex][0] == '/') // '/' == End dialogue
                    {
                        if (_dialogueLines[_dialogueIndex].Length == 1) //If the length is larger than one, then a unity event should be called
                        {
                            EndDialogue();
                        }
                        else //For events only (Finds the index of the event and calls the event accordingly.
                        {
                            string stringToInt = _dialogueLines[_dialogueIndex].Substring(1);
                            int eventIndex = int.Parse(stringToInt);
                            _dialogueInteractive.DialogueEndEvent[eventIndex].Invoke();

                            EndDialogue();
                        }

                        return;
                    }
                }

                //We can display the current line of dialogue, removing the character name
                string name = "";
                int startIndex = 0;

                for (int i = 0; i < dialogueLine.Length; i++)
                {
                    if (dialogueLine[i] != ':')
                    {
                        name += dialogueLine[i];
                    }
                    else
                    {
                        startIndex = i + 1;
                        break;
                    }
                }

                string dialogue = dialogueLine.Substring(startIndex);
                string finalDialogue = "";

                //We also need to remove the @ animation trigger and activate the animation using it
                for (int i = 0; i < dialogue.Length; i++)
                {
                    if (dialogue[i] == '@')
                    {
                        string animationTrigger = dialogue.Substring(i + 1);

                        break;
                    }
                    else
                    {
                        finalDialogue += dialogue[i];
                    }
                }

                Debug.Log(name + ":" + finalDialogue);

                if (name == "User")
                {
                    dialogueDisplay.SetActive(true);
                    dialogueText.text = finalDialogue;
                }
                else
                {
                    dialogueDisplay.SetActive(false);
                }

                continueDialogueButton.interactable = true;
            }

            _dialogueIndex++;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("Ending Dialogue");

        dialogueActive = false;

        if (_dialogueInteractive.DialogueCamera != null)
        {
            _dialogueInteractive.DialogueCamera.SetActive(false);
        }

        dialogueDisplay.SetActive(false);

        _dialogueInteractive = null;

        continueDialogueButton.gameObject.SetActive(false);

        _dialogueLines = new string[0];
        _dialogueIndex = 0;
    }

    public void SelectOption(int index) //Via Inspector (Button)
    {
        //Find the line which has the selected option

        int newLineIndex = 0;

        for (int i = 0; i < _dialogueLines.Length; i++)
        {
            if (_dialogueLines[i] == "/" + _optionArray[index] + "/")
            {
                newLineIndex = i + 1;
                break;
            }
        }

        _dialogueIndex = newLineIndex;

        optionsInterface.SetActive(false);
        NextDialogue();

        Debug.Log("Option: " + index + " selected");
    }

    private void DisplayDialogueOptions(string[] optionArray)
    {
        Debug.Log("Displaying dialogue options!");

        optionsInterface.SetActive(true);

        continueDialogueButton.interactable = false;
        dialogueDisplay.SetActive(false);

        for (int i = 0; i < optionTextArray.Length; i++)
        {
            bool optionExists = optionArray[i] != "";

            optionTextObjectArray[i].SetActive(optionExists);

            if (optionExists)
            {
                optionTextArray[i].text = optionArray[i];
            }
        }
    }
}
