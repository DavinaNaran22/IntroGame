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

    private PlayerInputActions inputActions;


    bool Drag = false;
    Vector3 og_position;
    bool connected = false;
    private int wire_count = 0;

    private void Awake() {

        if (Instance == null)
        {

            Instance = this;
           
        }
        inputActions = new PlayerInputActions();

    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Mouse_Down.performed += _ => Mouse_Down();
        inputActions.Player.Mouse_Up.performed += _ => Mouse_Up();
    }

    private void OnDisable()
    {
        inputActions.Player.Mouse_Down.performed -= _ => Mouse_Down();
        inputActions.Player.Mouse_Up.performed -= _ => Mouse_Up();
        inputActions.Player.Disable();
    }

    private void Mouse_Down()
    {
        // when the mouse button is down drag is set to true and ray cast hits collider
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D detect_collider = Physics2D.Raycast(mouse_pos, Vector2.zero);

        if (detect_collider.collider != null && detect_collider.collider.transform == transform)
        {
            Drag = true;
        }

    }

    private void Mouse_Up()
    {
        Drag = false;
        if (!connected)
        {
            Set_position(og_position);
        }

    }
    // sets the original position of wire to wire when it doesnt connect and drag is false 

    void Start()
    {
        og_position = transform.position;
     
    }

    // updates every frame to drag the object, changing its position every frame
    private void Update()

    {
      
        if (Drag) {

            connected = false;           
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
    //method to reset position of wire 
    void Set_position(Vector3 position_new) {
        transform.position = position_new;
        position_new.z = wiremiddle.transform.position.z;



        Vector3 pos_difference = position_new- wiremiddle.transform.position;
        wiremiddle.SetPosition(2, pos_difference);
       


    }

    // checks if the all the wires are connected using a count
    // count increments when wire instance is connected checks each time incremented

    public void WireConnected()
    {
        wire_count++;

        if (wire_count == 4) {
            Repaired = true;
            // turns the wire light color to green to indicate the player has repaired the wires 
            wirelight.color = Color.green;
        }
    }



}
