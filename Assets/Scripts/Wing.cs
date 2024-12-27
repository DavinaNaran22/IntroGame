using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    public bool Drag = false;
    public float z;
    public Plane new_plane;
    public Vector3 pos_offset;

    private void Update()
    {
        if (Drag == true) {
           // converting mouse position to ray
            Vector3 mousePos= Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (new_plane.Raycast(ray, out float distance))
            {
                transform.position = ray.GetPoint(distance) + pos_offset;
            }
        }
    }
    void OnMouseDown()
    {
        // when the object is clicked drag is enabled 
        Debug.Log("CLICKED");
        Drag = true;
        // creating a new plane (to move object on)
        new_plane = new Plane(Camera.main.transform.forward, transform.position);

        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        // if the ray intersects the plane
        if (new_plane.Raycast(ray, out float distance)) {
            // offset of the obj position - intersection point 
            pos_offset = transform.position - ray.GetPoint(distance);
        }
    }

    void OnMouseUp()
    {
        Drag = false;
    }






}
