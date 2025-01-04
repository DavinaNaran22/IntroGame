using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using UnityEngine.XR;
using Unity.VisualScripting;

public class RepairTask2 : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI promptText;
    public AlienRestrictScene alienRestrictScene;
    public GameObject takePicsScene;


    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;


    private PlayerInputActions inputActions;


    public List<string> additionalDialogues = new List<string>
    {
        "Next, I have to repair that hole in the ship. What can I use to do that?",
        "Hang on, I could ask somebody. Hopefully that alien over there is nice.",
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
        if (GameManager.Instance.complTaskTwo) transform.parent.gameObject.SetActive(false);
        Debug.Log("RepairTask2 Start");
        GameManager.Instance.Save();
        player = GameManager.Instance.player;
        //promptText.gameObject.SetActive(false);
        StartAdditionalDialogues();
        //takePicScript.SetActive(true);
    }

    private void Update()
    {
        if (alienRestrictScene.isPlayerNearby)
        {
            takePicsScene.SetActive(true);
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
            takePicsScene.SetActive(true);
            promptText.gameObject.SetActive(true);

        }
    }

    public bool IsDialogueActive()
    {
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }


}

