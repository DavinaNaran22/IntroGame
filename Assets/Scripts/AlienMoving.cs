using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMoving : MonoBehaviour
{

    public Transform Player;
    public float detectionRadius = 5f;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f; // Speed of rotation when pacing or facing the player
    public Vector2 paceAreaSize = new Vector2(5, 5);

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        Player = GameManager.Instance.player.transform;
        // Set the starting position as the center of the pacing area
        startPosition = transform.position;
        SetNewTargetPosition();
    }

    private void Update()
    {
        //base.GetPlayerTransform();
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Face the player if within the detection radius
            FacePlayer();
        }
        else
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

    private void FacePlayer()
    {
        // Calculate direction to the player
        Vector3 directionToPlayer = (Player.position - transform.position).normalized;
        directionToPlayer.y = 0; // Keep the rotation on the horizontal plane

        // Rotate smoothly to face the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
