using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Control_panel : MonoBehaviour
{
    public Collider Cockpit;
    public bool IN = false;
    public GameObject C_panel;
    public Canvas ship_status;
    public Canvas ship_map;
    public GameObject Message;
    public GameObject Message2;
    public bool task1_completed = false;


    // Start is called before the first frame update
    void Start()
    {
        Message.SetActive(false);
        C_panel.SetActive(false);
        Message2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"IN state: {IN}");

        if (IN == true && Input.GetKeyDown(KeyCode.A)) {
            //Debug.Log("ACTIVE");
            C_panel.SetActive(true);
            ship_map.enabled = false;
            Message.SetActive(false);
            task1_completed = true;

}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            IN = true;
            Message.SetActive(true);
            //Debug.Log("WERE IN");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IN = false;
            Message.SetActive(false);
            C_panel.SetActive(false);
            Message2.SetActive(true);
            //Debug.Log("WERE IN");
        }
    }

    public void enable_ship_map() {
        ship_map.enabled = true;
        ship_status.enabled = false;

    }
    public void enable_ship_status()
    {
        ship_map.enabled = false;
        ship_status.enabled = true;

    }

    public void disable_ship_map()
    {
        ship_map.enabled = false;
        ship_status.enabled = true;

    }
    public void disable_ship_status()
    {
        ship_map.enabled = true;
        ship_status.enabled = false;

    }



}
