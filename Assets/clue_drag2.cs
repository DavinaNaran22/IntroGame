using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class clue_drag2 : MonoBehaviour
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
        inputActions.Player.Mouse_Down.performed += _ => Mouse_Down2();
        inputActions.Player.Mouse_Up.performed += _ => Mouse_Up2();
    }

    private void OnDisable()
    {
        inputActions.Player.Mouse_Down.performed -= _ => Mouse_Down2();
        inputActions.Player.Mouse_Up.performed -= _ => Mouse_Up2();
        inputActions.Player.Disable();
    }

    private void Mouse_Down2()
    {
        // when the mouse button is down drag is set to true and ray cast hits collider
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D detect_collider = Physics2D.Raycast(mouse_pos, Vector2.zero);

        if (detect_collider.collider != null && detect_collider.collider.transform == transform)
        {
            Drag = true;
        }

    }

    private void Mouse_Up2()
    {
        if (Drag)
        {
            Drag = false;

            // Snap to correct position if it's in the correct collider
            if (Puzzle.Clue_in)
            {
                transform.position = correct_position;
            }
        }
    }

    private void Update()
    {
        if (Drag)
        {
            Vector3 position_new = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position_new.z = 0;
            transform.position = position_new;
        }
    }
}
