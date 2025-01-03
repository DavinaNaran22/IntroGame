using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using UnityEngine.XR;
using Unity.VisualScripting;

public class MissionManager : MonoBehaviour
{
    public GameObject player; 
    public BoxCollider restrictedArea; 
    public TextMeshProUGUI promptText; 
    public TextMeshProUGUI dialogueText;
    public CameraManagement cameraManagement;
    public TaskManager taskManager;


    private bool photoTaken = false;
    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;

    private Vector3 minBounds; // Minimum bounds of the restricted area
    private Vector3 maxBounds; // Maximum bounds of the restricted area
    private CharacterController characterController; 
    private PlayerInputActions inputActions;


    // Additional dialogues to display
    private List<string> additionalDialogues = new List<string>
    {
        "YOU: Hello, my spaceship's hit an asteroid, and this was the only nearby planet.",
        "YOU: I really need that thruster so I can repair my ship and leave.",
        "YOU: Would it be all right if I grabbed it quickly? I won't stay long.",
        "ALIEN: How dare you claim things from our land, you don't belong here.",
        "ALIEN: This strange object belongs to us. If you want it, you'll have to fight us."
    };


    // New input system for taking photos and dismissing dialogue
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.OpenCamera.performed += ctx => ToggleCamera(); // Press P to open camera
        inputActions.Player.TakePhoto.performed += ctx => TakePhoto(); // Press T to take a photo
        inputActions.Player.DismissDialogue.performed += ctx => DismissDialogue(); // Right click to dismiss dialogue
        inputActions.Player.ExitCamera.performed += ctx => ExitPhotoMode(); // Press E to exit camera mode
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.OpenCamera.performed -= ctx => ToggleCamera();
        inputActions.Player.TakePhoto.performed -= ctx => TakePhoto();
        inputActions.Player.DismissDialogue.performed -= ctx => DismissDialogue();
        inputActions.Player.ExitCamera.performed -= ctx => ExitPhotoMode();
    }


    private void Start()
    {
        GameManager.Instance.Save();
        player = GameManager.Instance.player;
        cameraManagement = GameManager.Instance.cameraManagement;
        taskManager = GameManager.Instance.taskManager;

        taskManager.SetTaskText("Find missing thruster");
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
        promptText.text = "Press P to open camera, and T to take a photo.";
    }

    private void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
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
        //photoTaken = true;
        //Debug.Log("Photo taken!");
        //RemoveRestriction();
        //HidePrompt(); // Hide the prompt after taking a photo

        if(cameraManagement != null)
        {
            if (cameraManagement.IsAnyTargetInFrame())
            {
                cameraManagement.TakeScreenshot(); // Trigger the screenshot functionality
                photoTaken = true;
                Debug.Log("Photo taken!");
                RemoveRestriction();
                HidePrompt();
                StartAdditionalDialogues();
            }
            else
            {
                Debug.Log("No targets within the frame. Screenshot not taken.");
            }

        }
        else
        {
            Debug.LogError("CameraManagement script is not assigned!");
        }
    }

    // Call camera management script to open the camera (P key)
    private void ToggleCamera()
    {
        if (cameraManagement != null)
        {
            cameraManagement.TogglePhotoMode();
        }
        else
        {
            Debug.LogError("CameraManagement script is not assigned!");
        }
    }

    // Call camera management script to exit the camera mode (escape key)
    private void ExitPhotoMode()
    {
        if (cameraManagement != null)
        {
            cameraManagement.ExitPhotoMode();
        }
        else
        {
            Debug.LogError("CameraManagement script is not assigned!");
        }
    }

    // Remove the restriction on player movement
    private void RemoveRestriction()
    {
        // Remove the restriction logic
        restrictedArea.enabled = false;
    }

    // Dialogue between player and alien
    private void StartAdditionalDialogues()
    {
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
        return dialogueText.gameObject.activeSelf;
    }

}
