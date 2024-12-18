using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAlienArea3 : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    private Collider boxCollider; // Reference to the box collider
    private bool isActive = false;
    public Vector3 positionOffset = new Vector3(0, 0, 20);

    void Start()
    {
        // Get the box collider component
        boxCollider = GetComponent<Collider>();

        // Ensure the collider is a trigger
        boxCollider.isTrigger = true;
    }

    void Update()
    {
        if (isActive && player != null)
        {
            KeepPlayerInsideBox();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the box
        if (other.CompareTag("Player"))
        {
            isActive = true;
            Debug.Log("Player is now inside the box.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Prevent the player from leaving if the box is active
        if (other.CompareTag("Player") && isActive)
        {
            Debug.Log("Player attempted to leave the box.");
        }
    }

    private void KeepPlayerInsideBox()
    {
        // Get the bounds of the box collider
        Bounds bounds = boxCollider.bounds;
        Vector3 offsetMin = bounds.min + positionOffset;
        Vector3 offsetMax = bounds.max + positionOffset;

        // Clamp the player's position within the bounds
        Vector3 clampedPosition = player.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, offsetMin.x, offsetMax.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, offsetMin.y, offsetMax.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, offsetMin.z, offsetMax.z);

        // Update the player's position
        player.position = clampedPosition;
    }


}
