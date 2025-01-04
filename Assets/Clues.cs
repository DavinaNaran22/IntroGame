using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clues : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.ShowClueScript = GameObject.Find("ShowClue").GetComponent<ShowClue>();
    }
}
