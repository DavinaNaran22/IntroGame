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
        Drag = true;
        
    }
    void Update()
    {
        if (Drag == true)
        {
            Vector3 position_new = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position_new.z = 0;

            transform.position = position_new;

           
        }
    }
    public void OnMouseUp()
    {
        Drag = false;
        if (Puzzle.Clue_in == true)
        {
            Drag = false;
            transform.position = correct_position;
        }
    }


}