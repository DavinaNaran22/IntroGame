using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygenbarshow : MonoBehaviour
{
    public GameObject OxygenTank;
    public GameObject OxygenBar;
    // Start is called before the first frame update
    void Start()
    {
        OxygenBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!OxygenTank.activeInHierarchy)
        {
            OxygenBar.SetActive(true);
        }
    }
}
