using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic class for unity Objects only
public class Singleton<T> : MonoBehaviour where T : Object
{
    // Public getter and private setter so other scripts can't change instance
    public static Singleton<T> Instance { get; private set; }

    private void Awake()
    {
        // If in instance already exists and it's not this one
        if (Instance != null && Instance != this)
        {
            Debug.Log("There already exists an instance of this");
            // Only want one instance at any times (hence singleton
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("No instance of this exists, so can create a new one");
            Instance = this;
        }
    }
}
