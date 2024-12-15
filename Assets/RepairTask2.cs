using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTask2 : MonoBehaviour
{
    public bool alien_killed = false;
    public GameObject player;
    public BoxCollider restrictedArea;
    private Vector3 minBounds; // Minimum bounds of the restricted area
    private Vector3 maxBounds; // Maximum bounds of the restricted area
    private CharacterController characterController;
    private bool isPlayerInside = false;
    // Start is called before the first frame update
    void Start()
    {
        

        RemoveRestriction();
        // Calculate the bounds of the restricted area
        minBounds = restrictedArea.bounds.min;
        maxBounds = restrictedArea.bounds.max;
        characterController = player.GetComponent<CharacterController>();
       
        if (characterController == null)
        {
            Debug.LogError("Player does not have a CharacterController component!");
        }

    }

    //void OnTriggerEnter(Collider other)

    //{
    //    Debug.Log($"Triggered by: {other.gameObject.name}");
    //    if (other.gameObject == player)
    //    {
    //        Debug.Log("Player entered the restricted area.");
    //        RestrictPlayerMovement();
    //    }
    //}



    // Update is called once per frame
    void Update()
    {
        if (player == null || restrictedArea == null) return;

        // Check if the player is inside the restricted area using bounding box check
        if (IsPlayerInsideRestrictedArea())
        {
            if (!isPlayerInside) // Only act if player has just entered
            {
                Debug.Log("Player entered the restricted area.");
                RestrictPlayerMovement();
                isPlayerInside = true; // Update the state
            }
        }
        else
        {
            if (isPlayerInside) // Detect exit if necessary
            {
                Debug.Log("Player left the restricted area.");
                isPlayerInside = false;
            }
        }
    }

    private bool IsPlayerInsideRestrictedArea()
    {
        // Check if player's position is within the bounds of the restricted area's collider
        return restrictedArea.bounds.Contains(player.transform.position);
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

    private void RemoveRestriction()
    {
        // Remove the restriction logic
        restrictedArea.enabled = false;
    }
}


