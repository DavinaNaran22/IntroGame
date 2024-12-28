using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wing_attached : MonoBehaviour
{
    public GameObject Wing;
    public GameObject Cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        Wing.SetActive(false);
        Cube.SetActive(false);

    }
}
