using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignClueScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ShowClueScript = GameObject.Find("ShowClue").GetComponent<ShowClue>();
        if (GameManager.Instance.canEnableShowClue) GameManager.Instance.ShowClueScript.enabled = true;
    }
}
