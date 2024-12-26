using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wire : MonoBehaviour
{
    public static Wire Instance;
    public Transform wire_end;
    public SpriteRenderer wirelight;
    public LineRenderer wiremiddle;
    public bool Repaired = false;
    
    bool Drag = false;
    Vector3 og_position;
    bool connected = false;
    private int wire_count = 0;

    private void Awake() {

        if (Instance == null)
        {

            Instance = this;
           
        }
       
    }

 
    // getting the original positon of the wire 

    void Start()
    {
        og_position = transform.position;
     
    }

    private void Update()

    {
      
        if (Drag) {

            connected = false;            //converts the mouse position converted to world values 
            Vector3 mousePostion = Input.mousePosition;
            Vector3 convertMousePosition = Camera.main.ScreenToWorldPoint(mousePostion);
            convertMousePosition.z = 0;
            Set_position(convertMousePosition);

            Vector3 wire_EndDifference = convertMousePosition - wire_end.position;
            if (wire_EndDifference.magnitude < 2) {
                Set_position(wire_end.position);
                Drag = false;
             
                if (connected == false)
                {
                    connected = true;
                    Wire.Instance.WireConnected();   
                        
                }


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

    public void WireConnected()
    {
        wire_count++;

        if (wire_count == 4) {
            Repaired = true;
            wirelight.color = Color.green;
        }
    }



}
