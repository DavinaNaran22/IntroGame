using UnityEngine;

public class TeleportCockpit : FindPlayerTransform
{
    // Bonus - show text when near

    [SerializeField] private Vector3 chairCoords;
    private PlayerMovement playerScript;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        GetPlayerTransform();
        playerScript = Player.GetComponent<PlayerMovement>();
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
        }
    }
}