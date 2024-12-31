using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WireRepair : MonoBehaviour
{
    public GameObject Player;
    public BoxCollider PART2;
    public bool playerin = false;
    public static Vector3 Player_Task5;
    public Canvas Message_wire;
    // Start is called before the first frame update
    void Start()
    {
        
        PART2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Storage_Scene.Tools_collected == true) {

            PART2.enabled = true;
        }

        if(playerin == true)
        {
            Debug.Log("True");
            Message_wire.enabled = true;
        }
      
        if (playerin == true && Input.GetKeyDown(KeyCode.R))
        {
            
            SceneManager.LoadScene("Game");
            Debug.Log("Loading game scene");
          
        }
    }

    public void OnTriggerEnter(Collider other)

    {
        Debug.Log("HEREEEE");
        if (other.CompareTag("Player") && Storage_Scene.Tools_collected == true)
        {
            Player_Task5 = Player.transform.position;
            Debug.Log("HEREEEE22222");
            playerin = true;
           
        }
    }

}
