using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage_Scene : MonoBehaviour
{
    public bool Task1 = false;
    public Control_panel control_panel;
    public BoxCollider OxygenTank1;
    public BoxCollider OxygenTank2;
    public BoxCollider OxygenTank3;

    public BoxCollider FirstAidKit1;
    public BoxCollider FirstAidKit2;
    public BoxCollider FirstAidKit3;
    

    public BoxCollider Gun;

    public BoxCollider ScrewDriver;
    public BoxCollider Tools;

    public bool Task2 = false;


    // Start is called before the first frame update
    void Start()
    {
        GameObject control = GameObject.Find("Cockpit_collider");
        control_panel = control.GetComponent<Control_panel>();
     
        deactivate_task2();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Task1 == true)
        {
            control_panel.Message2.SetActive(false);
            activate_task2();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Task2 = true;

        }
    }
    //deactivates the objects so that they dont start task2 before completing task 1 

    public void deactivate_task2()
    {

        OxygenTank1.enabled = false;
        OxygenTank2.enabled = false;
        OxygenTank3.enabled = false;


        FirstAidKit1.enabled = false;
        FirstAidKit2.enabled = false;
        FirstAidKit3.enabled = false;

        ScrewDriver.enabled = false;
        Tools.enabled = false;

        Gun.enabled = false;

    }

    public void activate_task2()
    {

      

        OxygenTank1.enabled = true;
        OxygenTank2.enabled = true;
        OxygenTank3.enabled = true;

        FirstAidKit1.enabled = true;
        FirstAidKit2.enabled = true;
        FirstAidKit3.enabled = true;

        ScrewDriver.enabled = true;
        Tools.enabled = true;


        Gun.enabled = true;
    }

 
}
