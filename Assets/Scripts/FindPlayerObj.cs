using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer<T> : MonoBehaviour where T : Object
{
    [SerializeField] protected Transform Player;
 
    // Start is called before the first frame update
    void Start()
    {
        // If player null then find it
        // Player might be null if player has moved between scenes
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
    }
}
