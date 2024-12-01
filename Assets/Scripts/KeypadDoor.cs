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
        Player = GameManager.player;
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
        Player = GameManager.player;
        // If player has unlocked the door and they enter trigger
        // Move them to new scene
        if (Vector3.Distance(this.transform.position, Player.GetComponent<Transform>().position) < 4)
        {
            if (GameManager.unlockedDoor)
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
    }

    private void ShowKeypad()
    {
        keypad.SetActive(true);
        playerScript.canMove = false;
        mouseLook.canLook = false;
    }
}