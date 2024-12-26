using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win_message : MonoBehaviour
{
    public Canvas message;
    public SpriteRenderer Light;
    // Start is called before the first frame update
    void Start()
    {
        message.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Light.color == Color.green) {

            message.enabled = true;
        }
    }
}
