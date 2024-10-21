using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    private MeshRenderer meshRenderer; // To open/close door

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // If player goes near door, open door
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            meshRenderer.enabled = false;
        }
    }

    // If player 'leaves' door, close door
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            meshRenderer.enabled = true;
        }
    }
}
