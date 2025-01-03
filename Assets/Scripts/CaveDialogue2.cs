using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaveDialogue2 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject redCrystal;
    public Transform player;
    public GameObject transition;
    public TaskManager taskManager;

    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private bool hasDialoguePlayed = false;
    private int currentDialogueIndex = 0;

    private PlayerInputActions inputActions;
    private CharacterController characterController;

    public List<string> additionalDialogues = new List<string>
    {
        "I finally defeated the Zyrog alien!",
        "The Xenos aliens can finally live in peace with no more ruler on this planet.",
        "Now I can uplift the curse by taking this red crystal out the core.",
    };

    private void Start()
    {
        player = GameManager.Instance.player.transform;
        taskManager = GameManager.Instance.taskManager;
        StartAdditionalDialogues();
    }

    private void Update()
    {
        if (redCrystal.activeSelf == false)
        {
            taskManager.SetTaskText("Power ship with crystal");
            Debug.Log("Crystal equipped");
            EquipCrystal();
        }
    }

    private void EquipCrystal()
    {
        //redCrystal.SetActive(false);
        ShowDialogue("Maybe I can use this crystal as a power source for my spaceship...");
        
    }

    private IEnumerator transitionScene()
    {
        yield return new WaitForSeconds(4f);
        transition.SetActive(false);
    }


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

    private void DismissDialogue()
    {
        if (!additionalDialoguesActive) return;

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
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }
}
