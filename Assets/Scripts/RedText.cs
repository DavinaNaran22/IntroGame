using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedText : MonoBehaviour
{
    void Start()
    {
        ColourDropdown.AddToTextList(ColourChangeColours.Red, GetComponent<TextMeshProUGUI>());
    }
}
