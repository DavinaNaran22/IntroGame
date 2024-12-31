using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject target1;
    public static bool Clue_in = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISON1");
        if (other.gameObject == target1)
        {
            Clue_in = true;
            Debug.Log("COLLISON2");
        }
    }
}