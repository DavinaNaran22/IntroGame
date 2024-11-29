using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadDoor : PlayerSceneTransition
{
    public bool unlockedDoor = false;
    public PlayerMovement playerScript;
    [SerializeField] private GameObject keypad;
    [SerializeField] private GameObject Player;
    private MouseLook mouseLook;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        // Call parent start
        playerScript = Player.GetComponent<PlayerMovement>();
        mouseLook = Player.GetComponentInChildren<MouseLook>();
        hasCheckCondition = true; // Need to check that keycode has been entered first
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.InteractDoor.performed += ctx => MoveToLandscape();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.InteractDoor.performed -= ctx => MoveToLandscape();
    }

    // Otherwise
    // Show keypad
    // etc.

    private void MoveToLandscape()
    {
        // If player has unlocked the door and they enter trigger
        // Move them to new scene
        if (unlockedDoor)
        {
            hasCheckCondition = false;
            Debug.Log("Moving to other scene because of keypad");
            base.LoadOtherScene(Player);
        }
        else
        {
            ShowKeypad();
        }
    }

    private void ShowKeypad()
    {
        keypad.SetActive(true);
        playerScript.canMove = false;
        mouseLook.canLook = false;
    }

    //private void MoveToLandscape()
    //{
    //    // If player is near door
    //    if (Vector3.Distance(this.transform.position, Player.position) < 5.5)
    //    {
    //        if (unlockedDoor)
    //        {
    //            playerScript.lockCoords = Player.position;
    //            playerScript.MoveTo(chairCoords);
    //            // Disable player movement
    //            playerScript.canMove = false;
    //            mouseLook.canLook = true;
    //        }
    //        else
    //        {
    //            // Show keypad
    //            keypad.SetActive(true);
    //            playerScript.canMove = false;
    //            mouseLook.canLook = false;
    //        }
    //    }
    //}
}

