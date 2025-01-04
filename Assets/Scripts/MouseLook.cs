using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour
{
    public static float mouseSensitivity = 5f; // Mouse sensitivity
    public Transform playerBody; // Player's body
    public bool canLook = true;

    float xRotation = 0f; // Rotation around x axis

    private PlayerInputActions inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        mouseSensitivity = GameManager.Instance.MouseSens;
        if (canLook)
        {// Getting the mouse input to move around x and y axis
            //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            Vector2 mouseDelta = inputActions.Player.Look.ReadValue<Vector2>();
            float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
            float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY; // Rotating the camera around the x axis
            // Clamping the rotation around the x axis to prevent the camera from flipping
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            // Rotating the player's body around the x axis
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
