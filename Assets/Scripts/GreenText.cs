using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreenText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.colourScript.AddToTextList(ColourChangeColours.Green, GetComponent<TextMeshProUGUI>());
    }
}
