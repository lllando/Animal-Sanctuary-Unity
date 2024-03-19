using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{ 
    public enum Characters
    {
        Leo,
        Ella
    }

    public Dialogue[] introductionDialogue = new Dialogue[]
    {

    };

    private Characters[] introductionDialogueSpeaker = new Characters[]
    {
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Ella,
        Characters.Ella,
        Characters.Ella,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Ella,
        Characters.Ella,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
        Characters.Ella,
        Characters.Ella,
        Characters.Leo,
        Characters.Leo,
        Characters.Leo,
    };

    private string[] introductionDialogueText = new string[]
    {
    "Coo-Coo",
    "(Hello! How are you, mate! Welcome to Africa. How you doin’?)",
    "(I’m sure you’re confused right now. Where are you? What is going on? Why is a pigeon talking to you?)",
    "(Well, don’t worry! I’m going to explain everything! Except for the pigeon part, can’t explain that!)",
    "Coo-Coo-Coo",
    "(First off, I’m Leo the pigeon. I’ll be here to help you whenever you need me.)",
    "(And you, you are here to help animals in need. Save them, help them, and release them back out into the wild.)",
    "Coo-Coo",
    "(Let’s start with this elephant.)",
    "(Her name is Ella, and it looks like she’s been separated from her herd.)",
    "BAROOOOOOOOOOOMPH",
    "(Ben? Ben! Where is my son? Where is my Ben?)",
    "(Oh…I’m so tired.)",
    "(Let’s see what she needs.)",
    "(Oh, she looks really exhausted. Let’s take her back to the sanctuary and help her.)",
    "Coo-Coo-Coo",
    "(Good thing we already have a habitat for her.)",
    "(Let’s give her some time to get used to her new surroundings.)",
    "(In the meantime, we should probably get some food and fill her water trough.)",
    "Coo-Coo",
    "(There we go. She already looks more relaxed.)",
    "(Let’s give her some pets.)",
    "BAROOOOOOOOOOOMPH",
    "(Thank you for all your help. It means a lot to me.)",
    "Coo-Coo",
    "(Oh, look at her. Looks like she is feeling way better.)",
    "(Maybe we can find her herd and reunite them.)",
    "Coo-Coo",
    "(We found them!)",
    "(Let’s release Ella and reunite her with her family.)",
    "BAROOOOOOOOOOOMPH",
    "(Ben! Oh my Ben, I found you!)",
    "Coo-Coo-Coo",
    "(Look at her go. The first animal you saved was a pure success!)",
    "(This really is your calling!)"
    };

    public TMPro.TextMeshProUGUI dialogueText;
    public Image speakerImage;
    public Sprite leoSprite;
    public Sprite ellaSprite;

    public int currentDialogueIndex = -1;

    public void NextDialogueInSequence(Dictionary<Characters, string> dialogue)
    {

    }

    public void UpdateDialogue()
    {
        currentDialogueIndex++;
        dialogueText.text = introductionDialogueText[currentDialogueIndex];

        Debug.Log("d" + currentDialogueIndex);
        Debug.Log("s" + introductionDialogue.Length);
        if (introductionDialogue[currentDialogueIndex-1].speaker == Characters.Leo)
        {
            speakerImage.sprite = leoSprite;
        }
        else if (introductionDialogue[currentDialogueIndex].speaker == Characters.Ella)
        {
            speakerImage.sprite = ellaSprite;
        }
    }

    private void Start()
    {
        if (introductionDialogueSpeaker.Length != introductionDialogueText.Length)
            Debug.LogError($"Speaker and amount of dialogues do not match. Speakers: {introductionDialogueSpeaker.Length} Dialogue: {introductionDialogueText.Length} ");

        introductionDialogue = new Dialogue[introductionDialogue.Length];
        Debug.Log("init of size " + introductionDialogue);

        for (int i = 0; i < introductionDialogue.Length; i++)
        {
            introductionDialogue[i].speaker = introductionDialogueSpeaker[i];
            introductionDialogue[i].text = introductionDialogueText[i];
        }

        UpdateDialogue();
    }
}

public struct Dialogue
{
    public DialogueSystem.Characters speaker;
    public string text;
}