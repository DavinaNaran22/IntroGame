using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to keep gameobject between scene loading

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
