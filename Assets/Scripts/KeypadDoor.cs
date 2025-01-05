using UnityEngine;

public class KeypadDoor : PlayerSceneTransition
{
    public PlayerMovement playerScript;
    public bool keypadShowing;
    [SerializeField] private GameObject keypad;
    private MouseLook mouseLook;
    private PlayerInputActions inputActions;
    private GameObject Player;
    private Transform playerTransform;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        keypadShowing = false;
        Player = GameManager.Instance.player;
        playerTransform = Player.GetComponent<Transform>();
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

    private void MoveToLandscape()
    {
        Player = GameManager.Instance.player;
        // If player is near door when they right click
        if (Vector3.Distance(transform.position, playerTransform.position) < 6)
        {
            // If player has unlocked the door, change scene
            if (GameManager.Instance.unlockedDoor)
            {
                hasCheckCondition = false;
                Debug.Log("Moving to other scene because of keypad");
                mouseLook.canLook = true;
                GameManager.Instance.hoverText.text = "Right click to leave ship";
                base.LoadOtherScene(Player);
            }
            else if (!keypadShowing)
            {
                ShowKeypad();
            }
        }
    }

    private void ShowKeypad()
    {
        // Show keypad on screen
        keypad.SetActive(true);
        keypadShowing = true;
        // Stop movement and ability to look around
        playerScript.ToggleMovement();
        mouseLook.canLook = false;
    }
}