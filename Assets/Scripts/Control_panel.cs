using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Control_panel : MonoBehaviour
{
    public TaskManager taskManager;
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
    bool task1Completed = false;





    // Start is called before the first frame update
    void Start()
    {
        // deactivates messages and enabled the repair map
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
        // When the player is in the cockpit if they press A task 1 is enabled/ first version of control panel 
        if (IN == true && Input.GetKeyDown(KeyCode.A) && !task1Completed)
        {
            //Debug.Log("ACTIVE");
            C_panel.SetActive(true);
            ship_map.enabled = false;
            Msg1.SetActive(false);
            task1_completed = true;
            taskManager.IncreaseProgress(7); // Increase progress by 7%
            taskManager.SetTaskText("Collect resources"); // Update the task description
            task1Completed = true;



        }
        // if the wire game is completed update the control panel to reflect repairs 
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
        // when player is detected trigger bool is set to true = in cockpit, trigger control panel set true
        if (other.CompareTag("Player"))
        {

            IN = true;
            Msg1.SetActive(true);
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // when exiting the cockpit IN is false (no longer in cockit) messages to user deactivated 
        if (other.CompareTag("Player"))
        {
            IN = false;
            Message.SetActive(false);
            C_panel.SetActive(false);
            Message2.SetActive(true);
            Msg1.SetActive(false);
            taskManager.SetTaskText("Collect resources"); // Update the task description
            //Debug.Log("WERE IN");
        }
        // when the wire game is done disable instruction
        if (win_message.win == true)
        {
            Message2.SetActive(false);
          

        }

    }

    // methods to switch between tabs within control panel, disabling one tab and enabling the other 
    
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
