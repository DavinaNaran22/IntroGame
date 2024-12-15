using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAlienArea : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    private CharacterController characterController;

    private Vector3 minBounds; // Minimum bounds of the alien area
    private Vector3 maxBounds; // Maximum bounds of the alien area
    private BoxCollider alienArea;

    private void Start()
    {
        player = GameManager.Instance.player;


        characterController = player.GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("Player does not have a CharacterController component!");
        }

        // Get the BoxCollider of the alien area
        alienArea = GetComponent<BoxCollider>();
        if (alienArea == null)
        {
            Debug.LogError("No BoxCollider found on the alien area!");
        }
    }


    // Runs once when player enters alien area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered alien area");
            EnableRestriction();
            RestrictPlayerMovement();
        }

    }

    //Runs every frame player is in alien area
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RestrictPlayerMovement();
        }
    }

    // Runs once when player exits alien area
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("Player exited alien area");
    //        DisableRestriction();
    //    }
    //}

    // Restrict player movement within the bounds of the alien area
    private void RestrictPlayerMovement()
    {
        Vector3 playerPosition = player.transform.position;

        // Clamp the player's position within the alien area's bounds
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

    // Enable restriction and calculate the bounds of the alien area
    private void EnableRestriction()
    {
        if (alienArea != null)
        {
            minBounds = alienArea.bounds.min;
            maxBounds = alienArea.bounds.max;
        }
    }

    // Disable restriction logic (if needed for future extension)
    private void DisableRestriction()
    {
        // Optional: Reset or change behavior when leaving the alien area
        Debug.Log("Movement restriction disabled");
    }
}
