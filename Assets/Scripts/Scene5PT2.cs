using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scene5PT2 : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI dialogueText;

    private bool dialogueShown = false;
    private bool finishedD;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;
    public GameObject map;
    public GameObject scene5;
    public GameObject scene5PT2;


    private PlayerInputActions inputActions;


    public List<string> additionalDialogues = new List<string>
    {
        "It looks like an entrance to somewhere.",
        "Could it be a cave?",
        "I'm not sure what the back of it means. But I'll take it with me.",
        "Maybe I should look for this and hopefully I can find a power source.",
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
        map.SetActive(true);
        GameManager.Instance.Save();
        player = GameManager.Instance.player;
        scene5.SetActive(false);
        StartAdditionalDialogues();
        
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

            EquipMap();

        }
    }

    public bool IsDialogueActive()
    {
        //return dialogueText.gameObject.activeSelf;
        return additionalDialoguesActive;
    }

    private void EquipMap()
    {
        if (Input.GetKeyDown(KeyCode.E)) // NEED TO CHANGE SO CLUE CAN BE EQUIPPED
        {
            Debug.Log("Map equipped");
            map.SetActive(false);
            scene5PT2.SetActive(false);
        }
    }
}
