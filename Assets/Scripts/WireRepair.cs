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
    public TextMeshProUGUI promptText;
    public bool playerin = false;
    public static Vector3 Player_Task5;
    public TextMeshProUGUI returnText;
    //public Canvas Message_wire;

    public GameObject stopWingAttached;

    // Start is called before the first frame update
    void Start()
    {
        
        stopWingAttached.SetActive(false);
        turnOffMessage.gameObject.SetActive(true);
        PART2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.N)) // Change to check if storage box is collected
        {
            turnOffMessage.gameObject.SetActive(false);
            turnOnMessage.gameObject.SetActive(true);
            StartCoroutine(promptTextShow());
        }




        //if (Storage_Scene.Tools_collected == true && wing_attached.WingTask == true) {

        //    PART2.enabled = true;
        //    turnOffMessage.gameObject.SetActive(false);
        //    turnOnMessage.gameObject.SetActive(true);

        //}

        //if(playerin == true)
        //{
        //    //Debug.Log("True");
        //    Message_wire.enabled = true;
        //}
      
        if (playerin == true && Input.GetKeyDown(KeyCode.R) && wing_attached.WingTask == true)
        {
            turnOnMessage.gameObject.SetActive(false);
            SceneManager.LoadScene("Game");
            SceneManager.sceneLoaded += OnSceneLoad;
            Debug.Log("Loading game scene");
            returnText.gameObject.SetActive(true);
            StartCoroutine(returnTextShow());
        }
    }

    private IEnumerator promptTextShow()
    {
        yield return new WaitForSeconds(2f);
        turnOnMessage.gameObject.SetActive(false);
        promptText.gameObject.SetActive(true);
    }

    private IEnumerator returnTextShow()
    {
        yield return new WaitForSeconds(5f);
        returnText.gameObject.SetActive(false);
    }


    // Disable player in game scene (messes with wires)
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.PlayerCanvas.SetActive(false);
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
