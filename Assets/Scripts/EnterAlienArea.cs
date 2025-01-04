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
    private PlayerInputActions inputActions;

    private CharacterController characterController;

    private Vector3 minBounds; // Minimum bounds of the alien area
    private Vector3 maxBounds; // Maximum bounds of the alien area
    private BoxCollider alienArea;

    private bool restrictionEnabled = true;
    private bool waitingForEquip = false;

    public AudioSource alienAreaAudio; // Audio to play in the alien area
    public AudioSource backgroundAudio; // Background audio to resume after the task is completed

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {

        inputActions.Player.Enable();
        inputActions.Player.Equip.performed += ctx => ONEquip();
    }

    private void OnDestroy()
    {
        inputActions.Player.Disable();
        inputActions.Player.Equip.performed += ctx => ONEquip();
    }

    private void ONEquip()
    {
        // Check if the player presses E while inside the thruster's box collider
        if (waitingForEquip)
        {
            if (thruster != null)
            {
                // Get the BoxCollider from the thruster
                BoxCollider thrusterCollider = thruster.GetComponent<BoxCollider>();

                if (thrusterCollider != null)
                {
                    // Check if the player's position is inside the collider's bounds
                    if (thrusterCollider.bounds.Contains(player.transform.position))
                    {
                        Debug.Log("Player is inside the thruster's box collider and pressed E");
                        EquipThruster();
                    }
                    else
                    {
                        Debug.Log("Player is not inside the thruster's box collider.");
                    }
                }
                else
                {
                    Debug.LogWarning("Thruster does not have a BoxCollider yet.");
                }
            }
            else
            {
                Debug.LogError("Thruster reference is null.");
            }
        }

    }

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

        // Ensure initial audio state
        if (alienAreaAudio) alienAreaAudio.Stop();
        if (backgroundAudio) backgroundAudio.Play();
    }

    private void Update()
    {
        if (silverCube.activeSelf == true && brownCube.activeSelf == true && restrictionEnabled)
        {
            Debug.Log("Blocks are visible, restriction disabled");
            DisableRestriction();
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

        // Play alien area audio and stop background audio
        if (alienAreaAudio) alienAreaAudio.Play();
        if (backgroundAudio) backgroundAudio.Stop();

    }

    //Runs every frame player is in alien area
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RestrictPlayerMovement();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left alien area");

            // Stop alien area audio and resume background audio
            if (alienAreaAudio) alienAreaAudio.Stop();
            if (backgroundAudio) backgroundAudio.Play();
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
        ShowDialogue("Maybe I can craft something using the metal and wood dropped to dig this thruster out. First, I need to equip them.");
        waitingForEquip = true;

    }

    private void EquipThruster()
    {
       
        HideDialogue();
        completedRepairText.SetActive(true); // 5 SECS AFTER, BELOW EXECUTES
        StartCoroutine(ActivateRepairTasksWithDelay()); 

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
