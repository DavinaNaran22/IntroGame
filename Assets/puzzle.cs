using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject target1;
    public Vector3 correct_position;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISON");
        if (other.gameObject == target1)
        {
            Debug.Log("COLLISON");
            target1.transform.position = correct_position;
        }
    }
}