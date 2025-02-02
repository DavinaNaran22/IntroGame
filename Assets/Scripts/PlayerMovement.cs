using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f; // Speed of player
    public float crouchSpeed = 6f; // Movement speed while crouching
    public float crouchHeight = 0f; // Height when crouching
    public const float normalHeight = 3f; // Normal height of the player

    Vector3 velocity; // Velocity of player
    public float gravity = -9.81f; // Gravity of player

    public Transform groundCheck; // Ground check object
    public float groundDistance = 0.4f; // Radius of sphere that checks for ground
    public LayerMask groundMask; // Objects sphere should check for
    bool isGrounded; // Is player on the ground

    public float jumpHeight = 3f; // Jump height
    
    bool isCrouching = false; // Tracks if the player is crouching

    public PlayerInputActions inputActions;
    private Vector2 movementInput;
    public bool inChair = false;

    public bool canMove = true;
    public Vector3 lockCoords; // Used for exiting cockpit

    bool movedControllerBack = true; // Need to move controller back

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;
        inputActions.Player.Jump.performed += ctx => Jump();
        inputActions.Player.Crouch.performed += ctx => ToggleCrouch();
        inputActions.Player.ExitChair.performed += ctx => ExitChair();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Move.performed -= ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled -= ctx => movementInput = Vector2.zero;
        inputActions.Player.Jump.performed -= ctx => Jump();
        inputActions.Player.Crouch.performed -= ctx => ToggleCrouch();
        inputActions.Player.ExitChair.performed -= ctx => ExitChair();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Interior")
        {
            crouchHeight = 2f; // Anything lower makes player fall through ground
        }
        else
        {
            crouchHeight = 0f;
        }
        // Checks if player is on the ground

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Resets velocity if player is on the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Forces player down to ground
        }

        // Adjust height and speed based on crouch state
        if (isCrouching)
        {
            controller.height = crouchHeight; // Set to crouch height
            speed = crouchSpeed; // Slow down while crouching
            movedControllerBack = false;
        }
        else
        {
            if (!movedControllerBack) // Need to move controller up so player doesn't fall through ground
            {
                controller.Move(Vector3.up * (normalHeight - crouchHeight) * 0.5f);
                controller.height = normalHeight; // Set to normal height
                speed = 12f; // Reset speed to normal
                movedControllerBack = true;
            }
        }
            
        // Gets input from player and moves player in direction they are facing
        Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.y;
        controller.Move(move * speed * Time.deltaTime);

        // Applies gravity to player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Makes player jump
    private void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Jump height
        }
    }

    // Toggles crouching state
    private void ToggleCrouch()
    {
        isCrouching = !isCrouching; // Toggle crouching state
    }

    // Enable player movement - quick bug fix method
    public void EnableMovement()
    {
        canMove = true;
        inputActions.Player.Move.Enable();
        inputActions.Player.Jump.Enable();
        inputActions.Player.Crouch.Enable();
    }

    // Toggle whether play is allowed to move
    public void ToggleMovement()
    {
        canMove = !canMove;
        if (canMove)
        {
            EnableMovement();
        }
        else
        {
            inputActions.Player.Move.Disable();
            inputActions.Player.Jump.Disable();
            inputActions.Player.Crouch.Disable();
        }
    }

    // Moves player directly to specified position
    public void MoveTo(Vector3 position)
    {
        this.transform.position = position;
        // Have to call this because character controller overrides player's position
        // And sometimes moving player (like above) doesn't work
        Physics.SyncTransforms();
    }

    // If movement is locked and allow player to move and if nesc. move them to previous coords
    private void ExitChair()
    {
        if (!canMove && inChair)
        {
            ToggleMovement();
            if (lockCoords != null)
            {
                // Move to previous position before movement was locked
                MoveTo(lockCoords);
            }
        }
    }
}
