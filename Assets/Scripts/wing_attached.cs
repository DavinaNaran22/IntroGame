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
    public TextMeshProUGUI Message1;
    public GameObject promptText;
    public static bool WingTask = false;

    public GameObject startWireRepair;
    
    
    // Start is called before the first frame update
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
        if (!WingTask == true) {
            Message1.gameObject.SetActive(false);
        }
        if (Storage_Scene.Tools_collected == true) {
            Message1.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        WingTask = true;
        Wing.SetActive(false);
        Cube.SetActive(false);
        promptText.SetActive(false);
        Message1.gameObject.SetActive(true);
        startWireRepair.SetActive(true);

    }




}
