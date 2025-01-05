using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.colourScript.AddToImageList(ColourChangeColours.Red, GetComponent<Image>());
    }
}
