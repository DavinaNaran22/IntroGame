using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System
using TMPro;

public class MissionManager : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public BoxCollider restrictedArea; // The area restricting movement
    public TextMeshProUGUI promptText; // Reference to the TextMeshProUGUI component
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI component for dialogue
    public InputAction cameraAction; // Reference to the Camera input action

    private bool photoTaken = false; // Tracks if the player has taken the photo
    private bool dialogueShown = false; // Tracks if the dialogue has been shown
    private Vector3 minBounds; // Minimum bounds of the restricted area
    private Vector3 maxBounds; // Maximum bounds of the restricted area
    private CharacterController characterController; // Player's CharacterController

    private void Start()
    {
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

        // Enable the camera action
        cameraAction.Enable();

        // Register the action's callback
        cameraAction.performed += OnCameraAction;
    }

    private void OnDestroy()
    {
        // Unregister the action's callback and disable the action
        cameraAction.performed -= OnCameraAction;
        cameraAction.Disable();
    }

    private void Update()
    {
        if (!photoTaken)
        {
            if (!dialogueShown)
            {
                RestrictPlayerMovement();

                // Wait for the player to left-click to dismiss dialogue
                if (Input.GetMouseButtonDown(0)) // Left mouse button
                {
                    HideDialogue();
                    dialogueShown = true;
                }
            }
            else
            {
                // Restrict player movement within the bounds
                RestrictPlayerMovement();

                // Show the photo prompt once the dialogue is dismissed
                ShowPrompt();
            }
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

    private void OnCameraAction(InputAction.CallbackContext context)
    {
        if (!photoTaken && dialogueShown)
        {
            TakePhoto();
        }
    }

    private void TakePhoto()
    {
        photoTaken = true;
        Debug.Log("Photo taken!");
        RemoveRestriction();
        HidePrompt(); // Hide the prompt after taking a photo
    }

    private void RemoveRestriction()
    {
        // Remove the restriction logic
        restrictedArea.enabled = false;
    }
}
