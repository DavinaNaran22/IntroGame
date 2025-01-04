using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class Clue_drag : MonoBehaviour
{
    public bool Drag = false;
    public Vector3 correct_position;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {

        inputActions.Player.Enable();
        inputActions.Player.Mouse_Down.performed += ctx => Mouse_Down();
        inputActions.Player.Mouse_Up.performed += ctx => Mouse_Up();
    }

    private void OnDestroy()
    {
      
        inputActions.Player.Mouse_Down.performed += ctx => Mouse_Down();
        inputActions.Player.Mouse_Up.performed += ctx => Mouse_Up();
        inputActions.Player.Disable();
    }
    public void Mouse_Down()
    {
        // when the mouse button is down drag is set to true and ray cast hits collider
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D detect_collider = Physics2D.Raycast(mouse_pos, Vector2.zero);

        if (detect_collider.collider != null && detect_collider.collider.transform == transform)
        {
            Drag = true;
        }
    }
    void Update()
    {
        // updates the drag variable/ clue object position to follow the map 
        if (Drag == true)
        {
            Vector3 position_new = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position_new.z = 0;

            transform.position = position_new;


        }
    }
    public void Mouse_Up()
    {
        // sets the drag to false when the mouse button is released 
        Drag = false;
        // if the clue is in the correct collider snap it in the correct position
        if (Puzzle.Clue_in == true)
        {
            Drag = false;
            transform.position = correct_position;
        }
    }


}

