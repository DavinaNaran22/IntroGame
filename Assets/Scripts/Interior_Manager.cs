using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interior_Manager : MonoBehaviour
{
    public Storage_Scene storage_scene;
    public Control_panel control_panel;
    public GameObject Passcode;
    public GameObject Message3;
    public GameObject Message4;
    //public GameObject MessagePuzzle;
    public Canvas Message_Exit;
    public BoxCollider passcode;
    public GameObject mini_map;
    public GameObject Exit_control_panel;

    // Start is called before the first frame update
    void Start()
    {
        //MessagePuzzle.SetActive(false);
        Message_Exit.enabled = false;
        Message3.SetActive(false);
        Message4.SetActive(false);
        Passcode.SetActive(false);
        GameObject storage = GameObject.Find("Storage_Collider"); 
        storage_scene = storage.GetComponent<Storage_Scene>();
        storage_scene.deactivate_task2();

        GameObject control = GameObject.Find("Cockpit_collider");
        control_panel = control.GetComponent<Control_panel>();

    }

    // Update is called once per frame
    void Update()
    {    // enables tasks after task 1 is completed 
        if (control_panel.task1_completed == true) {
            //if the control panel is off 
            if (!Exit_control_panel.activeSelf) {
                // and if the wire game is completed 
                if (win_message.win == true)
                {
                    Message4.SetActive(false);


                    //if (Input.GetKeyDown(KeyCode.R) && Puzzle.Puzzle_Complete == false) {
                    //    Debug.Log("PUZZLE");
                    //    MessagePuzzle.SetActive(false);
                    //    //SceneManager.LoadScene("Puzzle");
                    //    //SceneManager.sceneLoaded += OnSceneLoad;
                    //    //Debug.Log("Loading puzzle scene");
                    //    scene5PT2.SetActive(true);

                    //}
                }
                else {
                    //enables the directions and the map for task 2 
                    Message4.SetActive(true);
                    StartCoroutine(MiniMap_active(3f));
                    if (mini_map.activeSelf)
                    {
                        Message4.SetActive(false);
                        Message_Exit.enabled = true;
                    }
                }
               
           
            }
            // enables the storage collider user can now pick up items 
            storage_scene.Task1 = true;


           
        }
        // Disables the exit message once left the cockpit 
        if (control_panel.IN == false)
        {

            Message_Exit.enabled = false;
        }

        // when task 1 and task 2 is completed start the next task ans its instructions
        if (control_panel.task1_completed == true && storage_scene.Task2 == true)
        {
            Message3.SetActive(true);
            Passcode.SetActive(true);
            //Debug.Log("TASK1 AND 2 COMPLETE");
            if (Passcode.activeSelf == false) {
                Message3.SetActive(false);

            }

        }

      


    }


    //private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    //{
    //    GameManager.Instance.PlayerCanvas.SetActive(false);
    //    GameManager.Instance.player.SetActive(false);
    //}

    // delays the activation of the minimap to allow for scanning

    IEnumerator MiniMap_active(float delay)
    {
        yield return new WaitForSeconds(delay);

        mini_map.SetActive(true);
       
    }



}
