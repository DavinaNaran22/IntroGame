using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaveCamRestrict : MonoBehaviour
{
    public GameObject player;
    public BoxCollider restrictedArea;
    public BoxCollider alienArea;
    public GameObject promptText;
    public TextMeshProUGUI dialogueText;
    public CameraManagement cameraManagement;

    private bool photoTaken = false;
    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private bool hasDialogueBeenShown = false;
    private int currentDialogueIndex = 0;

    private Vector3 minBounds; // Minimum bounds of the alien area
    private Vector3 maxBounds; // Maximum bounds of the alien area
    private CharacterController characterController;
    private PlayerInputActions inputActions;

    private List<string> additionalDialogues = new List<string>
    {
        "YOU: This must be the Zyrog alien! I need to take a picture of it.",
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
        inputActions.Player.ExitCamera.performed += ctx => ExitPhotoMode(); // Press Escape to exit camera mode
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
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!photoTaken)
        {
            if (dialogueShown)
            {
                // Show the photo prompt once the dialogue is dismissed
                ShowPrompt();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player && !hasDialogueBeenShown)
        {
            Debug.Log("Player collided with restricted area");
            dialogueText.gameObject.SetActive(true);
            currentDialogueIndex = -1;
            additionalDialoguesActive = true;
            hasDialogueBeenShown = true;
            ShowNextDialogue();
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // When the player exits the collision
        if (collision.gameObject == player)
        {
            Debug.Log("Player has left the restricted area!");
            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void DismissDialogue()
    {
        if (additionalDialoguesActive)
        {
            ShowNextDialogue();
        }
        else if (!dialogueShown)
        {
            HideDialogue();
            dialogueShown = true;

            // Now show the prompt since all dialogues are dismissed
            ShowPrompt();
        }

    }

    private void HideDialogue()
    {
        dialogueText.gameObject.SetActive(false);
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

            if (!dialogueShown)
            {
                dialogueShown = true;
                ShowPrompt();
            }
        }
    }

    private void ShowPrompt()
    {
        // Display the photo prompt message
        promptText.gameObject.SetActive(true);
    }

    private void HidePrompt()
    {
        promptText.gameObject.SetActive(false);
    }


    public bool IsDialogueActive()
    {
        return dialogueText.gameObject.activeSelf;
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

    private void TakePhoto()
    {
        if (cameraManagement != null)
        {
            if (cameraManagement.IsAnyTargetInFrame())
            {
                cameraManagement.TakeScreenshot(); // Trigger the screenshot functionality
                photoTaken = true;
                Debug.Log("Photo taken!");
                HidePrompt();
                gameObject.SetActive(false);
                alienArea.gameObject.SetActive(true);


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





}
