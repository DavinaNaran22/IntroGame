using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wire : MonoBehaviour
{
    public Transform wire_end;
    public SpriteRenderer wirelight;
    public LineRenderer wiremiddle;
    
    bool Drag = false;
    Vector3 og_position;
    bool connected = false;
    
 


    void Start()
    {
        og_position = transform.position;
     
    }

    private void Update()

    {
        if (Drag) {

            Vector3 mousePostion = Input.mousePosition;
            Vector3 convertMousePosition = Camera.main.ScreenToWorldPoint(mousePostion);
            convertMousePosition.z = 0;
            Set_position(convertMousePosition);

            Vector3 wire_EndDifference = convertMousePosition - wire_end.position;
            if (wire_EndDifference.magnitude < 2) {
                Set_position(wire_end.position);
                Drag = false;
                connected = true;
                
            }

        }


    }

    void Set_position(Vector3 position_new) {
        transform.position = position_new;
        position_new.z = wiremiddle.transform.position.z; // Match the z-position




        Vector3 pos_difference = position_new- wiremiddle.transform.position;
        wiremiddle.SetPosition(2, pos_difference);

    }

    private void OnMouseDown()
    {
        Drag = true;
        
        

    }
    private void OnMouseUp()
    {
        Drag = false;
        if (!connected) {
            Set_position(og_position);
        }
        

        

    }



}
