using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_button : MonoBehaviour
{
    public GameObject Pause;
    public GameObject Background;
    // Start is called before the first frame update
    // when game starts the pause button is hidden
    void Start()
    {
        Pause.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Background.activeSelf)
        {
            Pause.gameObject.SetActive(false);
        }
        else
        {
            Pause.gameObject.SetActive(true);
        }
    }


}
