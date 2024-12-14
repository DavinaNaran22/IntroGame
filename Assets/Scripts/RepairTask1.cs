using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
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
        "Hello, my spaceship’s hit an asteroid, and this was the only nearby planet.",
        "I really need that thruster so I can repair my ship and leave.",
        "Can I quickly grab it? I won’t be long.",
        "How dare you claim our land, you don’t belong here.",
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

        // Get the CharacterController from the player
        characterController = player.GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("Player does not have a CharacterController component!");
        }

        // Show the initial dialogue
        ShowDialogue("I need to take a picture of these creatures living on the planet.");

        // Hide the prompt text at the start
        promptText.gameObject.SetActive(false);

        GreenAlienBehavior[] alienBehaviors = Object.FindObjectsByType<GreenAlienBehavior>(FindObjectsSortMode.None);

        foreach (GreenAlienBehavior alienBehavior in alienBehaviors)
        {
            if (alienBehavior != null)
            {
                alienBehavior.missionManager = this;
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
        RemoveRestriction();
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
