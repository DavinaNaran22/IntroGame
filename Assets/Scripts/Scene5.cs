using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Scene5 : MonoBehaviour
{
    public GameObject map1;
    public GameObject logic;

    public GameObject player;
    public TextMeshProUGUI dialogueText;
    public GameObject scene5;
    public TaskManager taskManager;

    private bool dialogueShown = false;
    private bool finishedD;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;
    public Canvas Message_Exit;

    private bool startedScene = false;
    private PlayerInputActions inputActions;


    public List<string> additionalDialogues = new List<string>
    {
        "Looks like the ship is finally repaired!",
        "SYSTEM: Power required for take-off.",
        "Oh no! Where do I find a power source?",
        "I guess I have no other option to look for something like that.",
        "What if I start by looking for that cave entrance that I pieced together?",
        "Hopefully I can find something useful there.",
    };


    // New input system for taking photos and dismissing dialogue
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.DismissDialogue.performed += ctx => DismissDialogue(); // Press D to dismiss dialogue
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.DismissDialogue.performed -= ctx => DismissDialogue();
    }

    private void Start()
    {
        Message_Exit.enabled = false;
        if (map1.activeInHierarchy) GameManager.Instance.Save(); // Only save in interior if this task ready
        player = GameManager.Instance.player;
        taskManager = GameManager.Instance.taskManager;
    }

    private void Update()
    {
        if (GameManager.Instance.completedSceneFive) return;
        if (map1.activeInHierarchy && GameManager.Instance.puzzleCompleted && !startedScene)
        //if (GameManager.Instance.puzzleCompleted && !startedScene)
        {
            logic.SetActive(true);
            taskManager.IncreaseProgress(10);
            StartAdditionalDialogues();
            startedScene = true;
        }
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

    // Dialogue between player
    public void StartAdditionalDialogues()
    {
        Debug.Log("Starting additional dialogues");
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
            // enable the exit cockpit message after main dialouge 
            Message_Exit.enabled = true;

            Debug.Log("All dialogues finished");
            taskManager.SetTaskText("Find the cave");
            finishedD = true;
            GameManager.Instance.completedSceneFive = true;
            scene5.SetActive(false);
        }
    }

    public bool IsDialogueActive()
    {
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }
}
