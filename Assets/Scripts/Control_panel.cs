using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Control_panel : MonoBehaviour
{
    public Collider Cockpit;
    public static bool IN = false;
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
     
        Msg1.SetActive(false);
        Message.SetActive(false);
        C_panel.SetActive(false);
        Message2.SetActive(false);
        Map2.SetActive(false);
        Map.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("IN");
        Debug.Log(win_message.win);

        if (IN == true && Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log("ACTIVE");
            C_panel.SetActive(true);
            ship_map.enabled = false;
            Msg1.SetActive(false);
            task1_completed = true;

        }

        if (win_message.win == true)
        {
            Debug.Log("Updating");
            Map.SetActive(false);
            Map2.SetActive(true);
            Status.text = "SHIP REPAIRED";
            Status_details.text = "WARNING: FUEL RESERVES LOW";
            



        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            IN = true;
            Msg1.SetActive(true);
            

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

        if (win_message.win == true)
        {
            Message2.SetActive(false);
     

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
