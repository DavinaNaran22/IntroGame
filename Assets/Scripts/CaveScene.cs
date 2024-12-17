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
    public GameObject minimap;

    private bool dialogueShown = false;
    private bool additionalDialoguesActive = false;
    private int currentDialogueIndex = 0;
    public bool isPlayerNearby = false;

    // Additional dialogues to display
    private List<string> additionalDialogues = new List<string>
    {
        "YOU: Oh no, not another alien I have to fight.",
        "ALIEN: WAIT! Don't kill me! I'm not going to hurt you.",
        "YOU: Give me a reason why I shouldn't kill you.",
        "YOU: All your other friends didn't hesitate to try to kill me.",
        "ALIEN: I'm sorry about that, but it's not their fault. Let me explain.",
        "YOU: You have a minute to talk.",
        "ALIEN: You might have noticed that our planet is very dead, with no life anymore. And there's a reason for that.",
        "ALIEN: Once we had a visitor arrive from the planet Zyrog, and we all welcomed him with open arms.",
        "ALIEN: We thought he wanted to get to know us and visit us for a short time.",
        "ALIEN: But that’s when we found out he had another agenda. He wanted to invade our planet Xenos and take over!",
        "ALIEN: Because he was stronger and more powerful than us, we had no choice but to listen to him.",
        "ALIEN: He made us become evil, he put a curse on everyone. He said until he dies, we will all be working under his ruling.",
        "ALIEN: But he is invincible, and nobody has dared to try and kill him.",
        "ALIEN: That’s why we don’t have any visitors because they don’t want to become a prisoner like the rest of us.",
        "ALIEN: That’s why I apologise for my friends because they will never purposely harm anyone, especially a visitor like you.",
        "YOU: I’m very sorry to hear that, I can’t believe that happened. But how come you haven’t become evil like the rest?",
        "ALIEN: That’s because I was the first to befriend him when he arrived. We became friends until he told me his plan to take over.",
        "ALIEN: He wanted my help, so I pretended to be on his side, so he didn’t put the curse on me too.",
        "YOU: That was very brave of you.",
        "ALIEN: That’s why I left those clues around, hoping if one day a visitor landed on Xenos, they will eventually find the cave and kill him.",
        "ALIEN: So now you can help by killing him to save us.",
        "YOU: That’s what those clues were! But how can I fight him? I don’t have many weapons. I only have a pocket knife and a gun with very few bullets.",
        "ALIEN: Don’t worry, I’ve been hiding this sword so you can kill him.",
        "ALIEN: It’s a very special sword, made using his blood I saved when I was healing him from his injuries.",
        "ALIEN: He is the only one who is strong enough to kill himself, so infusing the blood with the sword will ensure he dies.",
        "YOU: Okay, I’ll do it. I hope I can end this once and for all.",
        "ALIEN: Thank you so much! Good luck! But be prepared because he will not go down easily."
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

    // MiniMap and dialogue at the start of the scene cuts out
    private void Start()
    {
        dialogueText.gameObject.SetActive(true);
        ShowDialogue("Navigation cutting out.");
        StartCoroutine(DeactivateMiniMap(3f));
    }

    // Deactivate minimap and dialogue after a certain time
    private IEnumerator DeactivateMiniMap(float time)
    {
        yield return new WaitForSeconds(time);
        minimap.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        dialogueText.gameObject.SetActive(true);
        ShowDialogue("This look like a maze. I remember seeing something similar.");
        yield return new WaitForSeconds(time);
        dialogueText.gameObject.SetActive(false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // Player is in proximity
            StartAdditionalDialogues(); // Start the dialogue
            Debug.Log("Player is nearby");
        }
    }

    // Reset dialogue state when player leaves the proximity
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; 
            HideDialogue(); 
            dialogueShown = false;
            additionalDialoguesActive = false;
            currentDialogueIndex = 0;
            Debug.Log("Player left the proximity, resetting dialogue state.");
        }
    }


}
