using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaveScene : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    private PlayerInputActions inputActions;

    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;
    public bool isPlayerNearby = false;

    // Additional dialogues to display
    private List<string> additionalDialogues = new List<string>
    {
        "Hello, my spaceship’s hit an asteroid, and this was the only nearby planet.",
        "I really need that thruster so I can repair my ship and leave.",
        "Would it be all right if I grabbed it quickly? I won’t stay long.",
        "How dare you claim things from our land, you don’t belong here.",
        "This strange object belongs to us. If you want it, you’ll have to fight us."
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


    private void DismissDialogue()
    {
        if (!isPlayerNearby) return;

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
        Debug.Log("Showing dialogue: " + message);
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
        if (!isPlayerNearby)
        {
            Debug.Log("Player is not nearby, can't start dialogues");
            return;
        }
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
        }
    }

    public bool IsDialogueActive()
    {
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // Player is in proximity
            StartAdditionalDialogues(); // Start the dialogue
            Debug.Log("Player is nearby");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // Player is no longer in proximity
            HideDialogue(); // Hide dialogue when the player leaves
            dialogueShown = false; // Reset dialogue state
            additionalDialoguesActive = false; // Reset additional dialogues
        }
    }











}
