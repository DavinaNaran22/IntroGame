using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaveScene : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    private PlayerInputActions inputActions;

    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;
    public bool isPlayerNearby = false;

    // Additional dialogues to display
    private List<string> additionalDialogues = new List<string>
    {
        "Oh no, not another alien I have to fight.",
        "WAIT! Don't kill me! I'm not going to hurt you.",
        "Give me a reason why I shouldn't kill you.",
        "All your other friends didn't hesitate to try to kill me.",
        "I'm sorry about that, but it's not their fault. Let me explain.",
        "You have a minute to talk.",
        "You might have noticed that our planet is very dead, with no life anymore.",
        "And there's a reason for that.",
        "Once we had a visitor arrive from the planet Zyrog, and we all welcomed him with open arms.",
        "We thought he wanted to get to know us and visit us for a short time.",
        "But that’s when we found out he had another agenda.",
        "He wanted to invade our planet Xenos and take over!",
        "Because he was stronger and more powerful than us, we had no choice but to listen to him.",
        "He made us become evil, he put a curse on everyone.",
        "He said until he dies, we will all be working under his ruling.",
        "But he is invincible, and nobody has dared to try and kill him.",
        "That’s why we don’t have any visitors because they don’t want to become a prisoner like the rest of us.",
        "That’s why I apologise for my friends because they will never purposely harm anyone, especially a visitor like you.",
        "I’m very sorry to hear that, I can’t believe that happened. But how come you haven’t become evil like the rest?",
        "That’s because I was the first to befriend him when he arrived. We became friends until he told me his plan to take over.",
        "He wanted my help, so I pretended to be on his side, so he didn’t put the curse on me too.",
        "That was very brave of you.",
        "That’s why I left those clues around, hoping if one day a visitor landed on Xenos, they will eventually find the cave and kill him.",
        "So now you can help by killing him to save us.",
        "That’s what those clues were! But how can I fight him? I don’t have many weapons.",
        "I only have a pocket knife and a gun with very few bullets.",
        "Don’t worry, I’ve been hiding this sword so you can kill him.",
        "It’s a very special sword, made using his blood I saved when I was healing him from his injuries.",
        "He is the only one who is strong enough to kill himself, so infusing the blood with the sword will ensure he dies.",
        "Okay, I’ll do it. I hope I can end this once and for all.",
        "Thank you so much! Good luck! But be prepared because he will not go down easily."
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


    private void DismissDialogue()
    {
        if (!isPlayerNearby) return;

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
        Debug.Log("Showing dialogue: " + message);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // Player is in proximity
            StartAdditionalDialogues(); // Start the dialogue
            Debug.Log("Player is nearby");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // Player is no longer in proximity
            HideDialogue(); // Hide dialogue when the player leaves
            dialogueShown = false; // Reset dialogue state
            additionalDialoguesActive = false; // Reset additional dialogues
        }
    }











}
