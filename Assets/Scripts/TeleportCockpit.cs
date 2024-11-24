using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCockpit : FindPlayerTransform
{

    // If player is near door
    // And presses e
    // Then teleport player to cockpit
    // Can't move body, can only rotate camera

    // Can press escape to exit cockpit
    // Should go back to the outside of door

    // Bonus - show text when near

    [SerializeField] private Vector3 chairCoords; // 505.13 // Vector3(568.549011,11.0687943,505.584991)
    private PlayerMovement playerScript;
    //[SerializeField] private Vector3 corridorCoords;
    // Have script which teleports player?

    private void Start()
    {
        GetPlayerTransform();
        playerScript = Player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // If player is near door and presses e
        if (Vector3.Distance(this.transform.position, Player.position) < 5.5 && Input.GetKeyDown(KeyCode.E))
        {
            playerScript.lockCoords = Player.position;

            Debug.Log("Teleported");
            // Teleport to chair
            Player.position = chairCoords; // Doesn't always teleport player...
            // Disable player movement
            //playerScript.inputActions.Player.Disable();
            playerScript.ToggleMovement();
            //playerScript.SetPosition(chairCoords);
        }
    }

}