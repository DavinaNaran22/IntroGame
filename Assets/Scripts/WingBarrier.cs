using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class WingBarrier : MonoBehaviour
{
    public GameObject player;
    public BoxCollider restrictedArea;

    public TextMeshProUGUI dialogueText;

    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private bool hasDialogueBeenShown = false;
    private int currentDialogueIndex = 0;
    private bool isPlayerInRestrictedArea = false;

    private Vector3 minBounds; // Minimum bounds of the alien area
    private Vector3 maxBounds; // Maximum bounds of the alien area
    private CharacterController characterController;
    private PlayerInputActions inputActions;

    public List<string> additionalDialogues = new List<string>
    {
        "YOU: Okay, I need to move this wing to put it back where it belongs.",
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

    // Dialogues and prompts are hidden at the start
    private void Start()
    {
        player = GameManager.Instance.player;

        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {

    }

    // Check if the player is within the restricted area
    private void OnTriggerEnter()
    {
        if (!hasDialogueBeenShown)
        {
            Debug.Log("Player collided with restricted area");
            isPlayerInRestrictedArea = true;

            if (!hasDialogueBeenShown)
            {
                dialogueText.gameObject.SetActive(true);
                currentDialogueIndex = -1;
                additionalDialoguesActive = true;
                hasDialogueBeenShown = true;
                ShowNextDialogue();
            }
        }
    }

    private void OnTriggerExit()
    {

        isPlayerInRestrictedArea = false;
        Debug.Log("Player has left the restricted area!");

    }

    // Dismiss the dialogue when the player right clicks
    private void DismissDialogue()
    {
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

    private void HideDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
        }
    }

    private void ShowDialogue(string message)
    {
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = message;
    }

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
            dialogueShown = true;

        }
    }


    public bool IsDialogueActive()
    {
        return dialogueText.gameObject.activeSelf;
    }



}

