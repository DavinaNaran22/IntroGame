using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Clue_drag : MonoBehaviour
{
    public bool Drag = false;
    public Vector3 correct_position;

    public void OnMouseDown()
    {
        // when the mouse button is down drag is set to true 
        Drag = true;
        
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
    public void OnMouseUp()
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