using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorToggle : MonoBehaviour
{
    public bool lockCursor = true;
    public bool forceCursorVisible = false;
    public PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.ToggleCursor.performed += ctx => ToggleCursor();
        SceneManager.activeSceneChanged += CheckCursorScene;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.ToggleCursor.performed += ctx => ToggleCursor();
        SceneManager.activeSceneChanged -= CheckCursorScene;
    }

    // Need cursor to always be enabled in the scenes;
    private void CheckCursorScene(Scene cur, Scene next)
    {
        // If scene is wire or puzzle game, or they haven't done the drag/drop task
        if (next.name == "Game" || next.name == "Puzzle" || next.name == "landscape" && GameManager.Instance.completedTaskThree && !GameManager.Instance.completedTaskFour)
        {
            forceCursorVisible = true;
            lockCursor = false;
        }
        else
        {
            forceCursorVisible = false;
            lockCursor = true;
        }
    }

    private void ToggleCursor()
    {
        lockCursor = !lockCursor;
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

    void Update()
    {
        // These need to have cursor visible for better gameplay or for gameplay to work
        if (
            !forceCursorVisible && (GameObject.FindWithTag("EndingButton") != null && GameObject.FindWithTag("EndingButton").activeSelf 
            || GameObject.FindWithTag("PauseMenu") != null && GameObject.FindWithTag("PauseMenu").activeSelf 
            || GameObject.FindWithTag("InventoryCanvas") != null && GameObject.FindWithTag("InventoryCanvas").activeSelf 
            || GameObject.FindWithTag("Keypad") != null && GameObject.FindWithTag("Keypad").activeSelf)
            || GameObject.FindWithTag("WeaponCursor") != null && GameObject.FindWithTag("WeaponCursor").activeSelf)
        {
            lockCursor = false;
        } 
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
        // (Below) would prevent player from clicking pause/progress buttons 
        // Or cursor showing in wire/puzzle game
        //else
        //{
        //    lockCursor = true;
        //}
    }
}
