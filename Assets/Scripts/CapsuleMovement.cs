using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    public Transform player;
    public float followDistance = 5f; // Distance which capsule starts following
    public float moveSpeed = 3f; // Capsule speed following player
    private bool shouldFollow = false; // Flag to check if capsule should follow player

    private Collider capsuleCollider;
    private Rigidbody rb;

    void Start()
    {
        // Get the capsule's collider component
        //capsuleCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFollow)
        {
            // Move capsule towards player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // Check if player entered the capsule trigger
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered capsule trigger");
            shouldFollow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if player exited the capsule trigger
        if (other.CompareTag("Player"))
        {
            shouldFollow = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with capsule");
            // DECREASE HEALTH BAR
        }
    }
}
