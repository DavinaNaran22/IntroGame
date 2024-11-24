using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCockpit : FindPlayerTransform
{
    // Bonus - show text when near

    [SerializeField] private Vector3 chairCoords;
    private PlayerMovement playerScript;

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
            playerScript.MoveTo(chairCoords);
            // Disable player movement
            playerScript.ToggleMovement();
        }
    }

}