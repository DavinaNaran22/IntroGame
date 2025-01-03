using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public BoxCollider Camera;
    public BoxCollider Gun;
    public BoxCollider ScrewDriver;

    public BoxCollider Axe;
    public MeshCollider Saw;
    public MeshCollider Chisel;
    public MeshCollider Rasp;



 

    public bool Task2 = false;


    // Start is called before the first frame update
    void Start()
    {
  
        GameObject control = GameObject.Find("Cockpit_collider");
        control_panel = control.GetComponent<Control_panel>();

        deactivate_task2();


    }

    public void OnTriggerEnter(Collider other)
    { // when the player enters the storage room and task1 is completed
        
        
        if (other.CompareTag("Player") && Task1 == true)
        {
            //Debug.Log("SET TO TRUE TASK 1");
            // set the instruction to go to the storage room off 
            control_panel.Message2.SetActive(false);
            activate_task2();


        }
        // when the wing is attached enable the tools to repair the wing 
    }

    public void OnTriggerExit(Collider other)
    {
        //once exited the storage room,set the task2 to true indicated that task2 is done
        if (other.CompareTag("Player") && Task1 == true && PlayerNearEquipable.count == 11)
        {
            Debug.Log("Tools collected");
            Task2 = true;

        }


    }
    //deactivates the objects so that they dont start task2 before completing task 1 

    public void deactivate_task2()
    {
        Debug.Log("disabled");
        OxygenTank1.enabled = false;
        OxygenTank2.enabled = false;
        OxygenTank3.enabled = false;


        FirstAidKit1.enabled = false;
        FirstAidKit2.enabled = false;
        FirstAidKit3.enabled = false;

        ScrewDriver.enabled = false;
        Axe.enabled = false;
        Chisel.enabled = false;
        Saw.enabled = false;
        Rasp.enabled = false;
        Camera.enabled = false;
        Gun.enabled = false;

        DisableEquip<PlayerNearEquipable>(OxygenTank1.gameObject);
        DisableEquip<PlayerNearEquipable>(OxygenTank2.gameObject);
        DisableEquip<PlayerNearEquipable>(OxygenTank3.gameObject);

        DisableEquip<PlayerNearEquipable>(FirstAidKit1.gameObject);
        DisableEquip<PlayerNearEquipable>(FirstAidKit2.gameObject);
        DisableEquip<PlayerNearEquipable>(FirstAidKit3.gameObject);
        DisableEquip<PlayerNearEquipable>(Camera.gameObject);
        DisableEquip<PlayerNearEquipable>(ScrewDriver.gameObject);
        DisableEquip<PlayerNearEquipable>(Axe.gameObject);
        DisableEquip<PlayerNearEquipable>(Chisel.gameObject);
        DisableEquip<PlayerNearEquipable>(Saw.gameObject);
        DisableEquip<PlayerNearEquipable>(Rasp.gameObject);

        DisableEquip<PlayerNearEquipable>(Gun.gameObject);

    }
    //activates the objects once task1 is completed 
    public void activate_task2()
    {


        Debug.Log("ENabled");
        OxygenTank1.enabled = true;
        OxygenTank2.enabled = true;
        OxygenTank3.enabled = true;

        FirstAidKit1.enabled = true;
        FirstAidKit2.enabled = true;
        FirstAidKit3.enabled = true;

        ScrewDriver.enabled = true;

        Camera.enabled = true;

        Gun.enabled = true;


        EnableEquip<PlayerNearEquipable>(OxygenTank1.gameObject);
        EnableEquip<PlayerNearEquipable>(OxygenTank2.gameObject);
        EnableEquip<PlayerNearEquipable>(OxygenTank3.gameObject);

        EnableEquip<PlayerNearEquipable>(FirstAidKit1.gameObject);
        EnableEquip<PlayerNearEquipable>(FirstAidKit2.gameObject);
        EnableEquip<PlayerNearEquipable>(FirstAidKit3.gameObject);
        EnableEquip<PlayerNearEquipable>(Camera.gameObject);
        EnableEquip<PlayerNearEquipable>(ScrewDriver.gameObject);
        EnableEquip<PlayerNearEquipable>(Axe.gameObject);
        EnableEquip<PlayerNearEquipable>(Chisel.gameObject);
        EnableEquip<PlayerNearEquipable>(Saw.gameObject);
        EnableEquip<PlayerNearEquipable>(Rasp.gameObject);


        EnableEquip<PlayerNearEquipable>(ScrewDriver.gameObject);
        

        EnableEquip<PlayerNearEquipable>(Gun.gameObject);
    }

    private void DisableEquip<T>(GameObject obj) where T : MonoBehaviour {

        T EQUIP_script = obj.GetComponent<T>();
        if (EQUIP_script != null)
        {
            EQUIP_script.enabled = false;
        }

    }

    private void EnableEquip<T>(GameObject obj) where T : MonoBehaviour
    {

        T EQUIP_script = obj.GetComponent<T>();
        if (EQUIP_script != null)
        {
            EQUIP_script.enabled = true;
        }
        

    }

}
