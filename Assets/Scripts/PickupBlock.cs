using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupBlock : FindPlayerTransform
{
    public SpawnBox spawnBox;
    [SerializeField] private float pickUpRange = 3f;
    [SerializeField] private bool canBePicked = true;
    private Transform boxContainer;
    private Rigidbody boxRb;
    private PlayerController playerController; // To change num of boxes collected
    private bool isHoldingItem = false;
    private const float ADD_GROUND_Y = 0.26F; // To stop box from ending up halfway in the ground
    private PlayerInputActions inputActions;


    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Equip.performed += ctx => ToggleEquip();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Equip.performed -= ctx => ToggleEquip();
    }

    // Assign all the components related to the Player game object
    void AssignPlayerComponents()
    {
        base.GetPlayerTransform();
        if (playerController == null) playerController = Player.GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AssignPlayerComponents();
        boxContainer = GameObject.FindWithTag("BoxContainer").transform;
        boxRb = this.gameObject.GetComponent<Rigidbody>();
        boxRb.isKinematic = true; // So block doesn't move
    }

    // Returns position of ground or of transform if no ground 
    Vector3 getGroundPos()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            return hitInfo.point;
        }
        return transform.position;
    }

    // Returns layer below block as int
    int GetLayerBelow()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitInfo, Mathf.Infinity))
        {
            return hitInfo.transform.gameObject.layer;
        }
        return transform.gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        AssignPlayerComponents();
    }

    private void ToggleEquip()
    {
        Vector3 distanceToPlayer = Player.position - transform.position;
        // If player isn't holding anything and presses equip button
        if (!isHoldingItem && canBePicked && distanceToPlayer.magnitude <= pickUpRange)
        {
            Equip();
        }
        else if (isHoldingItem && inputActions.Player.Equip.IsPressed())
        {
            Drop();
        }
    }

    private void Equip()
    {
        isHoldingItem = true;
        transform.SetParent(boxContainer);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity); // box moved to same position as parent (boxContainer) w/o rotating it
    }

    public void Drop()
    {
        isHoldingItem = false;
        transform.SetParent(null);

        // If CanBePlacedOn is below, stop it from being grabbed and destroy it
        if (GetLayerBelow() == LayerMask.NameToLayer("CanBePlacedOn"))
        {
            PreventPickup();
        }
        else
        {
            // Actually drop the box
            Vector3 groundPos = getGroundPos();
            transform.SetPositionAndRotation(new Vector3(transform.position.x, groundPos.y + ADD_GROUND_Y, transform.position.z), Quaternion.identity);
            Scene activeScene = SceneManager.GetActiveScene();
            spawnBox.UpdateBoxDetails(this.transform.position, activeScene.name);
            // Acts as destory on load - can't figure out why this gets marked as destroy on load w/o this...
            SceneManager.MoveGameObjectToScene(this.gameObject, activeScene);
        }
    }

    // Prevents block from being picked up and destroys it
    void PreventPickup()
    {
        playerController.collectBoxEvent.Invoke();
        canBePicked = false;
        Destroy(this.gameObject);
        spawnBox.BoxDestroyed = true;
    }
}
