using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PickupBlock : FindPlayerTransform
{
    [SerializeField] private float pickUpRange = 3f;
    [SerializeField] private bool canBePicked = true;
    private Transform boxContainer;
    private Rigidbody boxRb;
    private PlayerController playerController; // To change num of boxes collected
    private bool isHoldingItem = false;
    private const float ADD_GROUND_Y = 0.26F; // To stop box from ending up halfway in the ground
    public SpawnBox spawnBox;

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
                spawnBox.BoxDestroyed = true;
            }
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
        Vector3 groundPos = getGroundPos();
        transform.SetPositionAndRotation(new Vector3(transform.position.x, groundPos.y + ADD_GROUND_Y, transform.position.z), Quaternion.identity);
        Scene activeScene = SceneManager.GetActiveScene();
        spawnBox.UpdateBoxDetails(this.transform.position, activeScene.name);
        // Acts as destory on load - can't figure out why this gets marked as destroy on load w/o this...
        SceneManager.MoveGameObjectToScene(this.gameObject, activeScene);
    }
}
