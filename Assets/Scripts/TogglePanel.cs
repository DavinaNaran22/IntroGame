using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject Panel;

    void Start()
    {
        Panel.SetActive(false);
    }

    public void ToggleTab()
    {
        Panel.SetActive(!Panel.activeSelf);
    }
}
