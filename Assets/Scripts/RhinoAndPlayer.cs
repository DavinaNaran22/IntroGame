using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RhinoAndPlayer : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;
    public bool isPlayerNearby = true;

    private CharacterController characterController;
    private PlayerInputActions inputActions;

    private List<string> additionalDialogues = new List<string>
    {
        "ALIEN: Well, well, well…",
        "ALIEN: What do we have here? I’ve heard there was a new visitor. I have been expecting you.",
        "PLAYER: I’ve heard a lot about you too.",
        "ALIEN: I bet you have. I’m a legend around here. After all, I own Xenos, don’t I?",
        "PLAYER: You mean invaded. Why don’t you go crawl back to your miserable planet, Zyrog?",
        "ALIEN: Ha! You think I’d ever go back? Why would I, especially since I own everyone and everything here. I’m the most powerful being in the universe!",
        "PLAYER: That’s because you put a curse on everyone! You turned everyone evil!",
        "ALIEN: HA, HA, HA! YES! I did do that! I’m very clever, aren’t I?",
        "PLAYER: Well, you won’t be laughing for long because I’m going to kill you and uplift the curse.",
        "ALIEN: You silly human! I am invincible! Nothing and nobody will ever be able to kill me.",
        "ALIEN: You see that glowing red crystal over there? It’s fused with the core of this planet. That crystal is the source of the curse binding everyone under my control. And now, you’re next on the list!",
        "PLAYER: So, I can just destroy this crystal, and everyone will be free? And obviously kill you in the process.",
        "ALIEN: You really aren’t as bright as you look. I won’t let you get anywhere near it!",
        "ALIEN: See, I was thinking of letting you live and just cursing you. But now, I’m thinking killing you will be an even better decision.",
        "PLAYER: You’ll regret ever setting foot on this planet. I’ll destroy your crystal, your curse, and you!",
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
        StartAdditionalDialogues();
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
        if (!isPlayerNearby)
        {
            Debug.Log("Player is not nearby, can't start dialogues");
            return;
        }
        Debug.Log("Starting additional dialogues");
        additionalDialoguesActive = true;
        currentDialogueIndex = 0;
        ShowDialogue(additionalDialogues[currentDialogueIndex]);
    }

    // Display the next dialogue in the list
    private void ShowNextDialogue()
    {
        if (!isPlayerNearby) // Stop dialogue if the player is no longer nearby
        {
            Debug.Log("Player moved away. Stopping dialogue.");
            additionalDialoguesActive = false;
            HideDialogue();
            return;
        }

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
