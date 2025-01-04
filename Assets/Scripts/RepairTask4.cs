using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using UnityEngine.XR;
using Unity.VisualScripting;

public class RepairTask4 : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    public GameObject wingDupe;


    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;

    public GameObject cube;


    private PlayerInputActions inputActions;


    public List<string> additionalDialogues = new List<string>
    {
        "Finally, I’m on the last task.",
        "I need to find the wing that came off when we crashed.",
        "It must be around the spaceship.",
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
        GameManager.Instance.ShowClueScript = GameObject.Find("ShowClue").GetComponent<ShowClue>();
        GameManager.Instance.Save();
        player = GameManager.Instance.player;
        Debug.Log("RepairTask4 Start");
        wingDupe.SetActive(false);
        player = GameManager.Instance.player;
        cube.SetActive(true);
        Debug.Log("REPAIRRRRR");
        StartAdditionalDialogues();
    }

    private void Update()
    {
        //if (task4part2.activeSelf == false && cube.activeSelf == false)
        //{
        //    startClue.SetActive(true);
        //}
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

        }
    }

    public bool IsDialogueActive()
    {
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }
}

