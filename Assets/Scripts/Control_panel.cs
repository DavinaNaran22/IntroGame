using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Control_panel : MonoBehaviour
{
    public Collider Cockpit;
    public bool IN = false;
    public GameObject C_panel;
    public Canvas ship_status;
    public Canvas ship_map;
    public GameObject Message;
    public GameObject Message2;
    public GameObject Msg1;
    public bool task1_completed = false;
    public TextMeshProUGUI Status;
    public TextMeshProUGUI Status_details;
    public GameObject Map;
    public GameObject Map2;
   



    // Start is called before the first frame update
    void Start()
    {
        Msg1.SetActive(true);
        Message.SetActive(false);
        C_panel.SetActive(false);
        Message2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"IN state: {IN}");

        if (IN == true && Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log("ACTIVE");
            C_panel.SetActive(true);
            ship_map.enabled = false;
            Message.SetActive(false);
            task1_completed = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IN = true;
            Message.SetActive(true);
            Debug.Log("WERE IN");
            if (win_message.win == true)
            {
                Debug.Log("Updating status message...");
                Status.text = "SHIP REPAIRED";
                Status_details.text = "WARNING: FUEL RESERVES LOW";
                Map.SetActive(false);
                Map2.SetActive(true);


            }

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
            Msg1.SetActive(false);
            //Debug.Log("WERE IN");
        }
    }

    public void enable_ship_map()
    {
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
