using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Used when swapping scenes to ensure things like Player, UI etc. are moved across scenes

    void Awake() // Called when script instance is being loaded
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
