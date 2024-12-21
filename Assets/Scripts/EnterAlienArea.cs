using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnterAlienArea : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public GameObject silverCube;
    public GameObject brownCube;
    public GameObject thruster;
    public GameObject shovel;
    public RepairTask2 repairTask2;
    public TextMeshProUGUI dialogueText;
    public GameObject completedRepairText;
    public MissionManager repairTask1;

    private CharacterController characterController;

    private Vector3 minBounds; // Minimum bounds of the alien area
    private Vector3 maxBounds; // Maximum bounds of the alien area
    private BoxCollider alienArea;

    private bool restrictionEnabled = true;
    private bool waitingForEquip = false;



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

    private void Update()
    {
        if (silverCube.activeSelf == true && brownCube.activeSelf == true && restrictionEnabled)
        {
            Debug.Log("Blocks are visible, restriction disabled");
            DisableRestriction();
        }

        if (waitingForEquip && Input.GetKeyDown(KeyCode.E))
        {
            EquipBlocks();
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
        restrictionEnabled = false;
        Debug.Log("Movement restriction disabled");
        alienArea.enabled = false;
        ShowDialogue("Maybe I can craft something using this metal and wood to dig this thruster out.");
        waitingForEquip = true;

    }

    private void EquipBlocks()
    {
        Debug.Log("waiting for E");
        silverCube.SetActive(false);
        brownCube.SetActive(false);

        if (shovel.activeSelf == true)
        {
            HideDialogue();
            Debug.Log("Dig thruster out"); // NEED BUTTON TO DIG OUT THRUSTER
            thruster.SetActive(false);
            completedRepairText.SetActive(true); // 5 SECS AFTER, BELOW EXECUTES
            StartCoroutine(ActivateRepairTasksWithDelay());
        }  
    }

    private IEnumerator ActivateRepairTasksWithDelay()
    {
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        Debug.Log("Activating repair tasks after delay");
        completedRepairText.SetActive(false); // Hide completedRepairText after delay, if needed
        repairTask1.gameObject.SetActive(false);
        repairTask2.gameObject.SetActive(true);
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


}
