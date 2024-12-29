using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Singleton which persists across scenes
// Generic class for unity Objects only
public class Singleton<T> : MonoBehaviour where T : UnityEngine.Object
{
    // Public getter and private setter so other scripts can't change instance
    public static T Instance { get; private set; }

    protected void Awake()
    {
        // If in instance already exists and it's not this one
        if (Instance != null && Instance != this)
        {
            // Only want one instance at any times (hence singleton)
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this as T;
        }
    }
}
