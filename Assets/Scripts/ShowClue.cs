using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowClue : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI dialogueText;

    private bool dialogueShown = false;
    private bool finishedD;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;
    public GameObject map;
    public GameObject task5;
    public GameObject showClue;
    public TaskManager taskManager;



    private PlayerInputActions inputActions;


    public List<string> additionalDialogues = new List<string>
    {
        "It looks like an entrance to somewhere.",
        "Could it be a cave?",
        "I'm not sure what the back of it means, maybe it's a maze.",
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
        if (GameManager.Instance.sawClueMap) Debug.Log("Disabling show clue script"); enabled = false; // Prevent activating multiple times
        map.SetActive(true);
        GameManager.Instance.Save();
        player = GameManager.Instance.player;
        taskManager = GameManager.Instance.taskManager;
        task5.SetActive(false);
        StartAdditionalDialogues();
        taskManager.IncreaseProgress(5);
        taskManager.SetTaskText("Review damage of spaceship");
        GameManager.Instance.sawClueMap = true;

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

            Debug.Log("All dialogues finished");

            map.SetActive(false);
            showClue.SetActive(false);

        }
    }

    public bool IsDialogueActive()
    {
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }


}
