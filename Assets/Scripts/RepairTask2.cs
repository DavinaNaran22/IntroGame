using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class RepairTask2 : MonoBehaviour
{
    public GameObject player;
    public BoxCollider restrictedArea;

    public TextMeshProUGUI promptText;
    public TextMeshProUGUI dialogueText;

    private bool photoTaken = false;
    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;

    private int currentDialogueIndex = 0;


    private Vector3 minBounds; // Minimum bounds of the restricted area
    private Vector3 maxBounds; // Maximum bounds of the restricted area
    private CharacterController characterController;
    private PlayerInputActions inputActions;


    private List<string> additionalDialogues = new List<string>
    {
        "Hello! I need some help.",
        "I’m trying to find some sort of metal alloy to repair a hole in my ship.",
        "Where can I find some?",
        "We have the most advanced technology in the galaxy.",
        "Our skin is made of strong metal alloy to make us indestructible.",
        "Now that you are here, I can use you for my next experiment.",
        "I must fight this alien before I get captured."
    };


    // New input system for taking photos and dismissing dialogue
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.TakePhoto.performed += ctx => TakePhoto();
        inputActions.Player.DismissDialogue.performed += ctx => DismissDialogue();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.TakePhoto.performed -= ctx => TakePhoto();
        inputActions.Player.DismissDialogue.performed -= ctx => DismissDialogue();
    }


    private void Start()
    {

        player = GameManager.Instance.player;

        // Calculate the bounds of the restricted area
        minBounds = restrictedArea.bounds.min;
        maxBounds = restrictedArea.bounds.max;

        restrictedArea.enabled = true;
        restrictedArea.isTrigger = false;

        // Get the CharacterController from the player
        characterController = player.GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("Player does not have a CharacterController component!");
        }

        dialogueShown = false;
        // Show the initial dialogue
        ShowDialogue("But first I need to take a picture.");

        // Hide the prompt text at the start
        promptText.gameObject.SetActive(false);

        GreenAlienBehavior[] alienBehaviors = UnityEngine.Object.FindObjectsByType<GreenAlienBehavior>(FindObjectsSortMode.None);

        foreach (GreenAlienBehavior alienBehavior in alienBehaviors)
        {
            if (alienBehavior != null)
            {
                alienBehavior.repairTask2 = this;
            }
        }
    }



    private void Update()
    {
        if (!photoTaken)
        {
            if (!dialogueShown)
            {
                // Restrict player movement within the bounds
                RestrictPlayerMovement();
            }
            else
            {
                RestrictPlayerMovement();

                // Show the photo prompt once the dialogue is dismissed
                ShowPrompt();
            }
        }
    }

    private void DismissDialogue()
    {
        if (!dialogueShown)
        {
            HideDialogue();
            dialogueShown = true;
        }

        else if (additionalDialoguesActive)
        {
            ShowNextDialogue();
        }
    }

    private void ShowPrompt()
    {
        // Display the photo prompt message
        promptText.gameObject.SetActive(true);
        promptText.text = "Press M to take a photo!";
    }

    private void HidePrompt()
    {
        // Hide the photo prompt message
        promptText.gameObject.SetActive(false);
    }

    private void ShowDialogue(string message)
    {
        // Display dialogue on the screen
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = message;
    }

    private void HideDialogue()
    {
        // Hide dialogue from the screen
        dialogueText.gameObject.SetActive(false);
    }

    private void RestrictPlayerMovement()
    {
        Vector3 playerPosition = player.transform.position;

        // Clamp the player's position within the restricted area's bounds
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(playerPosition.x, minBounds.x, maxBounds.x),
            playerPosition.y, // Keep Y-axis unchanged (CharacterController handles vertical movement)
            Mathf.Clamp(playerPosition.z, minBounds.z, maxBounds.z)
        );

        // If the position has been clamped, move the player back
        if (clampedPosition != playerPosition)
        {
            Vector3 movementOffset = clampedPosition - playerPosition;
            characterController.Move(movementOffset);
        }
    }


    private void TakePhoto()
    {
        photoTaken = true;
        Debug.Log("Photo taken!");
        restrictedArea.isTrigger = true;
        //RemoveRestriction();
        HidePrompt(); // Hide the prompt after taking a photo
        StartAdditionalDialogues();
    }

    private void RemoveRestriction()
    {
        // Remove the restriction logic
        restrictedArea.enabled = false;
    }



    private void StartAdditionalDialogues()
    {
        additionalDialoguesActive = true;
        currentDialogueIndex = 0;
        ShowDialogue(additionalDialogues[currentDialogueIndex]);
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
        }
    }

    public bool IsDialogueActive()
    {
        return dialogueText.gameObject.activeSelf;
    }
}
