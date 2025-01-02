using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkS5PT : MonoBehaviour
{
    public GameObject scene5;
    public GameObject scene5pt2;
    public GameObject puzzleMessage;

    // Update is called once per frame
    void Update()
    {
        if (scene5.activeSelf == false && puzzleMessage.activeSelf == false)
        {
            scene5pt2.SetActive(true);
        }
    }
}
