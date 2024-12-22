using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class EnterAlienArea4 : MonoBehaviour
{
    private Collider boxCollider;
    public Transform player;
    public GameObject alienDrop;
    public GameObject alienDrop2;
    public GameObject clue;
    public BoxCollider restrictPlayerCam;
    public TextMeshProUGUI dialogueText;
    public GameObject completedRepairText;
    public GameObject drawingsCompletedText;
    public RepairTask3 repairTask3;
    // public RepairTask4 repairTask4;

    private bool isActive = false;
    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private bool hasDialoguePlayed = false;
    private int currentDialogueIndex = 0;
    public bool isPlayerNearby = false;

    public Vector3 positionOffset = new Vector3(0, 0, 20);

    private CharacterController characterController;
    private PlayerInputActions inputActions;

    private bool waitingForEquipB = false;
    private bool waitingForEquipC = false;
    private bool blocksEquipped = false;

    public List<string> additionalDialogues = new List<string>
    {
        "ALIEN: I have heard so much about you.",
        "ALIEN: You are the one killing my friends.",
        "ALIEN: Now it�s your turn to die.",
        "YOU: I don't think so.",
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

        GAlienS3[] alien = UnityEngine.Object.FindObjectsByType<GAlienS3>(FindObjectsSortMode.None);
        foreach (GAlienS3 a in alien)
        {
            if (a != null)
            {
                a.enterAlienArea4 = this;
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

        if (alienDrop.activeSelf == true && alienDrop2.activeSelf == true)
        {
            Debug.Log("Block is visible, restriction disabled");
            ShowDialogue("YOU: Now I have a thermal conductor, I can repair the temperature conrol system!");
        }

        if (waitingForEquipB && Input.GetKeyDown(KeyCode.E)) // NEED TO CHANGE SO BLOCK CAN BE EQUIPPED
        {
            Debug.Log("Blocks equipped");
            EquipBlocks();
        }

        if (blocksEquipped && waitingForEquipC && Input.GetKeyDown(KeyCode.R)) // NEED TO CHANGE SO CLUE CAN BE EQUIPPED
        {
            Debug.Log("Clue equipped");
            EquipClue();
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
            waitingForEquipB = true;
        }

        if (clue.activeSelf == true)
        {
            waitingForEquipC = true;
        }
    }

    private void EquipBlocks()
    {
        Debug.Log("Blocks equipped");
        blocksEquipped = true;
        alienDrop.SetActive(false);
        alienDrop2.SetActive(false);
        HideDialogue();
        completedRepairText.SetActive(true);
        StartCoroutine(ActivateRepairTasksWithDelay());

    }

    private IEnumerator ActivateRepairTasksWithDelay()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Activating repair tasks after delay");
        completedRepairText.SetActive(false);
        boxCollider.isTrigger = true;
        ShowDialogue("YOU: Hang on, what is that strange drawing on that boulder over there? I should take it with me.");
    }

    private void EquipClue()
    {
        Debug.Log("Clue equipped");
        clue.SetActive(false);
        HideDialogue();
        drawingsCompletedText.SetActive(true);
        StartCoroutine(ActivateClueTasksWithDelay());
    }

    private IEnumerator ActivateClueTasksWithDelay()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Activating clue tasks after delay");
        drawingsCompletedText.SetActive(false);
        repairTask3.gameObject.SetActive(false);
        //repairTask4.gameObject.SetActive(true);
    }

    // Runs once when player enters alien area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
            isPlayerNearby = true;
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

    private void HideDialogue()
    {
        dialogueText.gameObject.SetActive(false);
    }

    // Dialogue between player and alien
    public void StartAdditionalDialogues()
    {
        if (hasDialoguePlayed) return;
        Debug.Log("Starting additional dialogues");
        hasDialoguePlayed = true;
        additionalDialoguesActive = true;
        currentDialogueIndex = 0;
        ShowDialogue(additionalDialogues[currentDialogueIndex]);
    }

    // Display the next dialogue in the list
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
        }
    }

    public bool IsDialogueActive()
    {
        return additionalDialoguesActive;
    }

}