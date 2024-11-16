using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    public CharacterController controller;

    public float speed = 12f; // Speed of player
    public float crouchSpeed = 6f; // Movement speed while crouching
    public float crouchHeight = 0f; // Height when crouching
    public float normalHeight = 3f; // Normal height of the player

    Vector3 velocity; // Velocity of player
    public float gravity = -9.81f; // Gravity of player

    public Transform groundCheck; // Ground check object
    public float groundDistance = 0.4f; // Radius of sphere that checks for ground
    public LayerMask groundMask; // Objects sphere should check for
    bool isGrounded; // Is player on the ground

    public float jumpHeight = 3f; // Jump height
    
    bool isCrouching = false; // Tracks if the player is crouching


    // Update is called once per frame
    void Update()
    {

        // Checks if player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Resets velocity if player is on the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Forces player down to ground
        }

        // Toggle crouch state when the "C" key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching; // Toggle crouching state
        }

        // Adjust height and speed based on crouch state
        if (isCrouching)
        {
            controller.height = crouchHeight; // Set to crouch height
            speed = crouchSpeed; // Slow down while crouching
        }
        else
        {
            controller.height = normalHeight; // Set to normal height
            speed = 12f; // Reset speed to normal
        }


        // Gets input from player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Moves player in direction they are facing
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Applies gravity to player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Makes player jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Jump height
        }
    }
}
