using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Clue_drag : MonoBehaviour
{

    private void OnMouseDrag()
    {
        Vector3 position_new = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position_new.z = 0;

        transform.position = position_new;
    }




}