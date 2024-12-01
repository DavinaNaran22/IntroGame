using UnityEngine;

public class TeleportCockpit : FindPlayerTransform
{
    // If they right click on door it should take them to cockpit
    [SerializeField] private Vector3 chairCoords;
    public PlayerMovement playerScript;
    private MouseLook mouseLook;
    private PlayerInputActions inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        GetPlayerTransform();
        playerScript = Player.GetComponent<PlayerMovement>();
        mouseLook = Player.GetComponentInChildren<MouseLook>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.InteractDoor.performed += ctx => MoveToCockpit();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.InteractDoor.performed -= ctx => MoveToCockpit();
    }

    // Move player to cockpit if they're near the door
    private void MoveToCockpit()
    {
        // If player is near door
        if (Vector3.Distance(this.transform.position, Player.position) < 5.5)
        {
            playerScript.lockCoords = Player.position;
            playerScript.MoveTo(chairCoords);
            // Disable player movement
            playerScript.ToggleMovement();
            mouseLook.canLook = true;
        }
    }
}