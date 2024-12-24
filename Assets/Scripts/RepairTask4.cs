using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using UnityEngine.XR;
using Unity.VisualScripting;

public class RepairTask4 : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI dialogueText;


    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;


    private PlayerInputActions inputActions;


    public List<string> additionalDialogues = new List<string>
    {
        "Onto the next task. How can I repair the temparature control system?",
        "Maybe I could follow the path with the blue flowers.",
        "Oh no, there’s an alien over there.",
        "What if I  use the alien skin to act as a thermal conductor?",
        "I need to kill this alien for that."
    };


    // New input system for taking photos and dismissing dialogue
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.DismissDialogue.performed += ctx => DismissDialogue(); // Press D to dismiss dialogue
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.DismissDialogue.performed -= ctx => DismissDialogue();
    }

    private void Start()
    {
        Debug.Log("RepairTask3 Start");
        player = GameManager.Instance.player;
        StartAdditionalDialogues();
    }

    private void Update()
    {

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

    // Dialogue between player
    public void StartAdditionalDialogues()
    {
        Debug.Log("Starting additional dialogues");
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

            Debug.Log("All dialogues finished");

        }
    }

    public bool IsDialogueActive()
    {
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }
}
