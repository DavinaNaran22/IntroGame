using UnityEngine;

public class KeypadDoor : PlayerSceneTransition
{
    public PlayerMovement playerScript;
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
        if (Vector3.Distance(this.transform.position, playerTransform.position) < 4)
        {
            // If player has unlocked the door, change scene
            if (GameManager.Instance.unlockedDoor)
            {
                hasCheckCondition = false;
                Debug.Log("Moving to other scene because of keypad");
                base.LoadOtherScene(Player);
            }
            else
            // Show them keypad
            {
                ShowKeypad();
            }
        }
    }

    private void ShowKeypad()
    {
        keypad.SetActive(true);
        playerScript.ToggleMovement();
        mouseLook.canLook = false;
    }
}