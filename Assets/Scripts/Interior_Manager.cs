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
    public GameObject MessagePuzzle;
    public BoxCollider passcode;
    public GameObject mini_map;
    public GameObject Exit_control_panel;
    public GameObject scene5PT2;

    // Start is called before the first frame update
    void Start()
    {
        MessagePuzzle.SetActive(false);

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
    {    
        if (control_panel.task1_completed == true) {
            if (!Exit_control_panel.activeSelf) {
                if (win_message.win == true)
                {
                    Message4.SetActive(false);
                    
                    MessagePuzzle.SetActive(true);
                    if (Puzzle.Puzzle_Complete == true){
                        MessagePuzzle.SetActive(false);
                        scene5PT2.SetActive(true);
                    }
                    if (Input.GetKeyDown(KeyCode.R) && Puzzle.Puzzle_Complete == false) {
                        MessagePuzzle.SetActive(false);
                        SceneManager.LoadScene("Puzzle");
                        SceneManager.sceneLoaded += OnSceneLoad;
                        Debug.Log("Loading puzzle scene");
                       

                    }
                }
                else {
                    Message4.SetActive(true);
                    StartCoroutine(MiniMap_active(3f));
                    if (mini_map.activeSelf)
                    {
                        Message4.SetActive(false);
                    }
                }
               
           
            }
            storage_scene.Task1 = true;


           
        }

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


    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.PlayerCanvas.SetActive(false);
        GameManager.Instance.player.SetActive(false);
    }



    IEnumerator MiniMap_active(float delay)
    {
        yield return new WaitForSeconds(delay);

        mini_map.SetActive(true);
       
    }



}
