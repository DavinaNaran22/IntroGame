using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WireRepair : MonoBehaviour
{
    public GameObject Player;
    public BoxCollider PART2;
    public TextMeshProUGUI turnOffMessage;
    public TextMeshProUGUI turnOnMessage;
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
            turnOffMessage.gameObject.SetActive(false);
            turnOnMessage.gameObject.SetActive(true);

        }

        if(playerin == true)
        {
            //Debug.Log("True");
            Message_wire.enabled = true;
        }
      
        if (playerin == true && Input.GetKeyDown(KeyCode.R))
        {
            turnOnMessage.gameObject.SetActive(false);
            SceneManager.LoadScene("Game");
            SceneManager.sceneLoaded += OnSceneLoad;
            Debug.Log("Loading game scene");
          
        }
    }

    // Disable player in game scene (messes with wires)
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.player.SetActive(false);
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
