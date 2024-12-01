using UnityEngine;

public class TeleportCockpit : MonoBehaviour
{
    // If they right click on door it should take them to cockpit
    [SerializeField] private Vector3 chairCoords;
    public PlayerMovement playerScript;
    private MouseLook mouseLook;
    private PlayerInputActions inputActions;
    private Transform playerTransform;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        GameObject player = GameManager.Instance.player;
        playerScript = player.GetComponent<PlayerMovement>();
        mouseLook = player.GetComponentInChildren<MouseLook>();
        playerTransform = player.GetComponent<Transform>();
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
        if (Vector3.Distance(this.transform.position, playerTransform.position) < 5.5)
        {
            playerScript.lockCoords = playerTransform.position;
            playerScript.MoveTo(chairCoords);
            // Disable player movement
            playerScript.ToggleMovement();
            playerScript.inChair = true;
            mouseLook.canLook = true;
        }
    }
}