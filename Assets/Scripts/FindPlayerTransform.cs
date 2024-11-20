using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayerTransform: MonoBehaviour 
{
    [SerializeField] protected Transform Player;
 
    // Protected so can only be accessed in derived class
    protected void GetPlayerTransform()
    {
        // If player null then find it
        // Player might be null if player has moved between scenes
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
    }
}
