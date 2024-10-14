using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    // If player goes near door, open door
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            Debug.Log("Door closing");
        }
    }

    // If player 'leaves' door, close door
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            Debug.Log("Door opening");
        }
    }
}
