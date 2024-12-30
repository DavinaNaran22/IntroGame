using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public static float mouseSensitivity = 100f; // Mouse sensitivity
    public Transform playerBody; // Player's body
    public bool canLook = true;

    float xRotation = 0f; // Rotation around x axis


    // Start is called before the first frame update
    void Start()
    {
        //Locking the cursor in the center of the screen
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseSensitivity = GameManager.Instance.MouseSens;
        if (canLook)
        {// Getting the mouse input to move around x and y axis
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY; // Rotating the camera around the x axis


            // Clamping the rotation around the x axis to prevent the camera from flipping
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotating the player's body around the x axis
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
