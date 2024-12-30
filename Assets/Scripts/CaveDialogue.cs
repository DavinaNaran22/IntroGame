using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaveDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private bool hasDialoguePlayed = false;
    private int currentDialogueIndex = 0;

    private PlayerInputActions inputActions;

    private List<string> additionalDialogues = new List<string>
    {
        "ALIEN: You should've listened when I told you I am invincible.",
        "ALIEN: Now you will die a painful death.",
        "YOU: I will not let you win!",
        "Wait, that purple stone the alien talked about earlier...",
        "It said it was something this Zyrog alien brought with him.",
        "What if I use it with my sword?",
        "It's worth a shot.",
    };

    private void Start()
    {
        StartAdditionalDialogues();
    }


    // New input system for taking photos and dismissing dialogue
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.DismissDialogue.performed += ctx => DismissDialogue(); // Right click to dismiss dialogue
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.DismissDialogue.performed -= ctx => DismissDialogue();
    }

    private void DismissDialogue()
    {
        if (!additionalDialoguesActive) return;

        if (additionalDialoguesActive)
        {
            ShowNextDialogue();
        }
        else
        {
            HideDialogue();
            dialogueShown = true;
        }
    }

    private void ShowDialogue(string message)
    {
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = message;
    }

    private void HideDialogue()
    {
        dialogueText.gameObject.SetActive(false);
    }

    // Dialogue between player and alien
    public void StartAdditionalDialogues()
    {
        if (hasDialoguePlayed) return;

        hasDialoguePlayed = true;
        additionalDialoguesActive = true;
        currentDialogueIndex = 0;
        ShowDialogue(additionalDialogues[currentDialogueIndex]);
    }

    // Display the next dialogue in the list
    private void ShowNextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < additionalDialogues.Count)
        {
            ShowDialogue(additionalDialogues[currentDialogueIndex]);
        }
        else
        {
            additionalDialoguesActive = false;
            HideDialogue();
        }
    }

    public bool IsDialogueActive()
    {
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }
}
