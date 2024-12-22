using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class EnterAlienArea2 : MonoBehaviour
{
    private Collider boxCollider;
    public Transform player;
    public GameObject alienDrop;
    public BoxCollider restrictPlayerCam;
    public TextMeshProUGUI dialogueText;
    public GameObject completedRepairText;
    public RepairTask2 repairTask2;
    //public RepairTask3 repairTask3;

    private bool isActive = false; 
    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private bool hasDialoguePlayed = false;
    private int currentDialogueIndex = 0;
    public bool isPlayerNearby = false;

    public Vector3 positionOffset = new Vector3(0, 0, 20);

    private CharacterController characterController;
    private PlayerInputActions inputActions;

    private bool waitingForEquip = false;


    public List<string> additionalDialogues = new List<string>
    {
        "YOU: Hello! I need some help.",
        "YOU: I�m trying to find some sort of metal alloy to repair a hole in my ship.",
        "YOU: Where can I find some?",
        "ALIEN: We have the most advanced technology in the galaxy.",
        "ALIEN: Our skin is made of strong metal alloy to make us indestructible.",
        "ALIEN: Now that you are here, I can use you for my next experiment.",
        "YOU: I must fight this alien before I get captured."
    };

    // New input system for taking photos and dismissing dialogue
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.DismissDialogue.performed += ctx => DismissDialogue(); // Right click to dismiss dialogue
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.DismissDialogue.performed -= ctx => DismissDialogue();
    }

    private void Start()
    {
        player = GameManager.Instance.player.transform;

        characterController = player.GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("Player does not have a CharacterController component!");
        }

        // Get the box collider component
        boxCollider = GetComponent<Collider>();

        // Ensure the collider is a trigger
        boxCollider.isTrigger = true;
        restrictPlayerCam.isTrigger = true;
        //dialogueText.gameObject.SetActive(true);

        GAlienS2[] alien = UnityEngine.Object.FindObjectsByType<GAlienS2>(FindObjectsSortMode.None);
        foreach (GAlienS2 a in alien)
        {
            if (a != null)
            {
                a.enterAlienArea2 = this;
            }
        }
    }

    private void Update()
    {
        if (isActive && player != null)
        {
            KeepPlayerInsideBox();
        }

        dialogueText.gameObject.SetActive(true);


        if (alienDrop.activeSelf == true)
        {
            Debug.Log("Blocks are visible, restriction disabled");
            DisableRestriction();
        }

        if (waitingForEquip && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Blocks equipped");
            EquipBlocks();
        }
    }

    private void DisableRestriction()
    {
        isActive = false;
        Debug.Log("Restriction disabled");

        if (alienDrop.activeSelf == true)
        {
            Debug.Log("Block is visible, restriction disabled");
            ShowDialogue("YOU: Now I have metal, I can use this to repair the hole!");
            waitingForEquip = true;
        }
    }

    private void EquipBlocks()
    {
        Debug.Log("Blocks equipped");
        alienDrop.SetActive(false);
        HideDialogue();
        completedRepairText.SetActive(true); // 5 SECS AFTER, BELOW EXECUTES
        StartCoroutine(ActivateRepairTasksWithDelay());
    }

    private IEnumerator ActivateRepairTasksWithDelay()
    {
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        Debug.Log("Activating repair tasks after delay");
        completedRepairText.SetActive(false); // Hide completedRepairText after delay, if needed
        repairTask2.gameObject.SetActive(false);
        //repairTask3.gameObject.SetActive(true);
    }


    // Runs once when player enters alien area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
            isPlayerNearby = true;
            //StartAdditionalDialogues();
            Debug.Log("Player is now inside the box.");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // Prevent the player from leaving if the box is active
        if (other.CompareTag("Player") && isActive)
        {
            isPlayerNearby = false;
            Debug.Log("Player attempted to leave the box.");
        }
    }

    private void KeepPlayerInsideBox()
    {
        // Get the bounds of the box collider
        Bounds bounds = boxCollider.bounds;
        Vector3 offsetMin = bounds.min + positionOffset;
        Vector3 offsetMax = bounds.max + positionOffset;

        // Clamp the player's position within the bounds
        Vector3 clampedPosition = player.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, offsetMin.x, offsetMax.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, offsetMin.y, offsetMax.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, offsetMin.z, offsetMax.z);

        // Update the player's position
        player.position = clampedPosition;
    }

    private void DismissDialogue()
    {
        if (!isPlayerNearby || !additionalDialoguesActive) return;

        if (additionalDialoguesActive)
        {
            ShowNextDialogue();
        }
        else
        {
            HideDialogue();
            dialogueShown = true;
        }
    }



    private void ShowDialogue(string message)
    {
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = message;
    }

    public void HideDialogue()
    {
        dialogueText.gameObject.SetActive(false);
    }

    // Dialogue between player and alien
    public void StartAdditionalDialogues()
    {
        if (hasDialoguePlayed) return;

        //if (!isPlayerNearby)
        //{
        //    Debug.Log("Player is not nearby, can't start dialogues");
        //    return;
        //}
        Debug.Log("Starting additional dialogues");
        hasDialoguePlayed = true;
        additionalDialoguesActive = true;
        currentDialogueIndex = 0;
        ShowDialogue(additionalDialogues[currentDialogueIndex]);

    }

    // Display the next dialogue in the list
    private void ShowNextDialogue()
    {
        //if (!isPlayerNearby) // Stop dialogue if the player is no longer nearby
        //{
        //    Debug.Log("Player moved away. Stopping dialogue.");
        //    additionalDialoguesActive = false;
        //    HideDialogue();
        //    return;
        //}

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
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }



}

