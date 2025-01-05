using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.colourScript.AddToLightList(ColourChangeColours.Red, GetComponent<Light>());
    }
}
