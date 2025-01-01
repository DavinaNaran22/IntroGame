using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Scene5 : MonoBehaviour
{
    public GameObject map1;
    public GameObject puzzleText;
    public GameObject logic;

    public GameObject player;
    public TextMeshProUGUI dialogueText;
    public GameObject scene5;

    private bool dialogueShown = false;
    private bool finishedD;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;

    private PlayerInputActions inputActions;


    public List<string> additionalDialogues = new List<string>
    {
        "Looks like the ship is finally repaired!",
        "SYSTEM: Power required for take-off.",
        "Oh no! Where do I find a power source?",
        "Wait, I have these three pieces of drawings I found earlier. Could that help?",
        "It looks to be some sort of puzzle.",
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
        GameManager.Instance.Save();
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        if (map1.activeInHierarchy)
        {
            logic.SetActive(true);
            StartAdditionalDialogues();
        }

        if (!puzzleText.activeInHierarchy && finishedD == true)
        {
            logic.SetActive(false);
            scene5.SetActive(true);

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

            Debug.Log("All dialogues finished");
            finishedD = true;
            puzzleText.SetActive(true);
            scene5.SetActive(false);


        }
    }

    public bool IsDialogueActive()
    {
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }
}
