using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupBlock : FindPlayerTransform
{
    private Transform boxContainer;
    private Rigidbody boxRb;
    private PlayerController playerController; // To change num of boxes collected
    [SerializeField] private float pickUpRange = 3f;
    private bool isHoldingItem = false;
    [SerializeField] private bool canBePicked = true;
    private const float ADD_GROUND_Y = 0.26F; // To stop box from ending up halfway in the ground
    public Scene scenePlacedIn;

    // Assign all the components related to the Player game object
    void AssignPlayerComponents()
    {
        base.GetPlayerTransform();
        playerController = Player.GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AssignPlayerComponents();
        boxContainer = GameObject.FindWithTag("BoxContainer").transform;
        boxRb = this.gameObject.GetComponent<Rigidbody>();
        boxRb.isKinematic = true; // So block doesn't move
        scenePlacedIn = SceneManager.GetActiveScene();
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

        Vector3 distanceToPlayer = Player.position - transform.position;
        // If player isn't holding anything and presses grab button
        if (!isHoldingItem && canBePicked && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E)) // https://www.youtube.com/watch?v=8kKLUsn7tcg
        {
            Equip();
        }

        // If player is holding something and presses grab button
        else if (isHoldingItem && Input.GetKeyDown(KeyCode.E))
        {
            isHoldingItem = false;
            transform.SetParent(null);

            // If CanBePlacedOn is below, stop it from being grabbed
            if (GetLayerBelow() == LayerMask.NameToLayer("CanBePlacedOn"))
            {
                playerController.collectBoxEvent.Invoke();
                canBePicked = false;
                Destroy(this.gameObject);
            }
            Drop();
        }
    }

    private void Equip()
    {
        isHoldingItem = true;
        transform.SetParent(boxContainer);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity); // box moved to same position as parent (boxContainer) w/o rotating it
        scenePlacedIn = SceneManager.GetActiveScene();
        Debug.Log("Player is holding an item");
    }

    private void Drop()
    {
        Vector3 groundPos = getGroundPos();
        transform.SetPositionAndRotation(new Vector3(transform.position.x, groundPos.y + ADD_GROUND_Y, transform.position.z), Quaternion.identity);
        Debug.Log("Player has dropped their item");
        KeepBlockInScene();
    }

    // After dropping block, it should stay in the scene it's currently in
    // I.e. shouldn't 'respawn' in scene it originated in
    private void KeepBlockInScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.MoveGameObjectToScene(this.gameObject, activeScene);
        scenePlacedIn = activeScene;
    }
}
