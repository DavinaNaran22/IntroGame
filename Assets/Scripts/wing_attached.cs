using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class wing_attached : MonoBehaviour
{
    public GameObject Wing;
    public GameObject Cube;
    //public TextMeshProUGUI Message1;
    public GameObject promptText;
    public static bool WingTask = false;

    public GameObject startWireRepair;
    
    
    // Start is called before the first frame update
    // once the wing is attatched, hide the wing and the target cube 
    void Start()
    {
        //Message1.enabled = false;
        if (WingTask == true) {
            Wing.SetActive(false);
            Cube.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // when the wing isnt attached disable the message prompting the user to next scene
        if (!WingTask == true) {
            //Message1.gameObject.SetActive(false);
        }
        // when the tools are collected deactivate the message prompting the user to next scene
        if (Storage_Scene.Tools_collected == true) {
            //Message1.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //once player enters the wire scene, deactivate the objects from the last/ start wire scene

        WingTask = true;
        Wing.SetActive(false);
        Cube.SetActive(false);
        promptText.SetActive(false);
        //Message1.gameObject.SetActive(true);
        startWireRepair.SetActive(true);

    }




}
