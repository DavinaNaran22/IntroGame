using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    public BoxCollider restrictedArea; // The area restricting movement
    public string promptMessage = "Press M to take a photo!"; // Message shown to the player
    private bool photoTaken = false; // Tracks if the player has taken the photo
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
    }

    private void Update()
    {
        if (!photoTaken)
        {
            ShowPrompt();

            // Restrict player movement within the bounds
            RestrictPlayerMovement();

            if (Input.GetKeyDown(KeyCode.M))
            {
                TakePhoto();
            }
        }
    }

    private void ShowPrompt()
    {
        // Display the prompt message (you can replace this with your UI logic)
        Debug.Log(promptMessage);
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
    }

    private void RemoveRestriction()
    {
        // Remove the restriction logic
        restrictedArea.enabled = false;
    }
}
