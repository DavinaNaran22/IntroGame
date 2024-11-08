using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMoving : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f; // Speed of rotation
    public Vector2 paceAreaSize = new Vector2(5, 5);

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        // Set the starting position as the center of the pacing area
        startPosition = transform.position;
        SetNewTargetPosition();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > detectionRadius)
        {
            // Continue pacing if the player is outside the detection radius
            PaceAround();
        }
    }

    private void PaceAround()
    {
        // Calculate direction to the target position
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Rotate towards the target direction
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if we reached the target position, and set a new one if so
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    private void SetNewTargetPosition()
    {
        // Choose a random point within the defined area
        float offsetX = Random.Range(-paceAreaSize.x / 2, paceAreaSize.x / 2);
        float offsetZ = Random.Range(-paceAreaSize.y / 2, paceAreaSize.y / 2);
        targetPosition = startPosition + new Vector3(offsetX, 0, offsetZ);
    }

    // Optional: Debugging tool to visualize the pacing area
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(startPosition, new Vector3(paceAreaSize.x, 0, paceAreaSize.y));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
