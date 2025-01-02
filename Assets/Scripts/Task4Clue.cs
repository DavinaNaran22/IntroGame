using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Task4Clue : MonoBehaviour
{
    public Transform player;
    public GameObject clue;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI drawingsCompletedText;
    public GameObject turnOff;
    public GameObject caveOn;

    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private bool hasDialoguePlayed = false;
    private int currentDialogueIndex = 0;
    public bool isPlayerNearby = false;


    private CharacterController characterController;
    private PlayerInputActions inputActions;



    // New input system for taking photos and dismissing dialogue
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
       
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();

    }

    private void Start()
    {
        player = GameManager.Instance.player.transform;

        characterController = player.GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("Player does not have a CharacterController component!");
        }

        ActivateRepairTasksWithDelay();
    }

    private void Update()
    {
        dialogueText.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E)) // NEED TO CHANGE SO CLUE CAN BE EQUIPPED
        {
            Debug.Log("Clue equipped");
            EquipClue();
        }

    }


    private void ActivateRepairTasksWithDelay()
    {
        Debug.Log("Activating repair tasks after delay");
        ShowDialogue("YOU: How weird, this is the third boulder I’ve seen with a weird drawing on it. I guess I should take that with me too.");
    }

    private void EquipClue()
    {
        Debug.Log("Clue equipped");
        clue.SetActive(false);
        HideDialogue();
        drawingsCompletedText.gameObject.SetActive(true);
        StartCoroutine(ActivateClueTasksWithDelay());
    }

    private IEnumerator ActivateClueTasksWithDelay()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Activating clue tasks after delay");
        drawingsCompletedText.gameObject.SetActive(false);
        ShowDialogue("YOU: Now I can go back in the ship and assess the damage.");
        yield return new WaitForSeconds(5);
        caveOn.SetActive(true);
        turnOff.SetActive(false);
    }

    private void HideDialogue()
    {
        dialogueText.gameObject.SetActive(false);
    }

    private void ShowDialogue(string message)
    {
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = message;
    }
}
