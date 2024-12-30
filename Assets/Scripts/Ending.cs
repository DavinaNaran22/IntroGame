using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ending : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject button;

    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private bool hasDialoguePlayed = false;
    private int currentDialogueIndex = 0;

    private CharacterController characterController;
    private PlayerInputActions inputActions;

    private List<string> additionalDialogues = new List<string>
    {
        "System: Spaceship powering and ready for takeoff.",
        "YOU: Yes! It works!",
        "System: Destination set to Earth.",
        "System: Initiating countdown.",
        "System: 10.",
        "System: 9.",
        "What a journey it has been. I'm glad I landed on Xenos and helped the aliens out.",
        "System: 8.",
        "System: 7.",
        "YOU: I hope they can bring back the much needed life to the planet.",
        "System: 6.",
        "System: 5.",
        "YOU: At least I will always have the pictures I took of the aliens as a keepsake.",
        "System: 4.",
        "System: 3.",
        "YOU: But I am looking forward to going back home.",
        "System: 2.",
        "System: 1.",
        "System: Takeoff.",
        "System: Spaceship has left the planet.",
        "YOU: Goodbye, Xenos. I hope to see you again someday.",
    };

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

    void Start()
    {
        ShowDialogue("YOU: Let’s see if the crystal can power this spaceship.");
        // NEED TO ADD A BUTTON THAT WILL LET THE CRYSTAL LEAVE INVENTORY
        button.SetActive(true);
        //HideDialogue();
        StartAdditionalDialogues();
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

        Debug.Log("Starting additional dialogues");
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
