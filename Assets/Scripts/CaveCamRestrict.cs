using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaveCamRestrict : MonoBehaviour
{
    public GameObject player;
    public BoxCollider restrictedArea;
    public GameObject promptText;
    public CameraManagement cameraManagement;

    private bool photoTaken = false;
    private bool restrictionEnabled = true;

    private Vector3 minBounds; // Minimum bounds of the alien area
    private Vector3 maxBounds; // Maximum bounds of the alien area

    private CharacterController characterController;
    private PlayerInputActions inputActions;

    // New input system for taking photos and dismissing dialogue
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    //private void OnEnable()
    //{
    //    inputActions.Player.Enable();
    //    inputActions.Player.OpenCamera.performed += ctx => ToggleCamera(); // Press P to open camera
    //    inputActions.Player.TakePhoto.performed += ctx => TakePhoto(); // Press T to take a photo
    //    inputActions.Player.DismissDialogue.performed += ctx => DismissDialogue(); // Press D to dismiss dialogue
    //    inputActions.Player.ExitCamera.performed += ctx => ExitPhotoMode(); // Press E to exit camera mode
    //}

    //private void OnDisable()
    //{
    //    inputActions.Player.Disable();
    //    inputActions.Player.OpenCamera.performed -= ctx => ToggleCamera();
    //    inputActions.Player.TakePhoto.performed -= ctx => TakePhoto();
    //    inputActions.Player.DismissDialogue.performed -= ctx => DismissDialogue();
    //    inputActions.Player.ExitCamera.performed -= ctx => ExitPhotoMode();
    //}

    private void Start()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            Debug.Log("Player collided with restricted area");
            if (promptText != null)
            {
                
                promptText.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // When the player exits the collision
        if (collision.gameObject == player)
        {
            Debug.Log("Player has left the restricted area!");
            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
        }
    }






}
