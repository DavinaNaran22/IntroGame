using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class WireRepair : MonoBehaviour
{
    public GameObject Player;
    public BoxCollider PART2;
    public TextMeshProUGUI turnOffMessage;
    public TextMeshProUGUI turnOnMessage;
    public TextMeshProUGUI promptText;
    public bool playerin = false;
    public static Vector3 Player_Task5;
    public GameObject StartClue;
    private PlayerInputActions inputActions;
    //public Canvas Message_wire;

    public GameObject stopWingAttached;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable(){

        inputActions.Player.Enable();
        inputActions.Player.Repair.performed += ctx => ToggleRepair();
    }

    private void OnDestroy()
    {
        inputActions.Player.Disable();
        inputActions.Player.Repair.performed += ctx => ToggleRepair();
    }
    private void ToggleRepair()
    {
        if (playerin == true && wing_attached.WingTask == true)
        {
            turnOnMessage.gameObject.SetActive(false);
            SceneManager.LoadScene("Game");
            SceneManager.sceneLoaded += OnSceneLoad;
            Debug.Log("Loading game scene");
            StartClue.SetActive(true);

        }
    }
        // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.Instance.player;
        stopWingAttached.SetActive(false);
        turnOffMessage.gameObject.SetActive(true);
        PART2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.N)) // Change to check if storage box is collected
        //{
        //    turnOffMessage.gameObject.SetActive(false);
        //    turnOnMessage.gameObject.SetActive(true);
        //    StartCoroutine(promptTextShow());
        //}




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
      // when the player is in the collider, wing is attached and the key r is pressed game scene activates and the clue scene is activated to be used when back to interior scene 
        //if (playerin == true && Input.GetKeyDown(KeyCode.R) && wing_attached.WingTask == true)
        //{
        //    turnOnMessage.gameObject.SetActive(false);
        //    SceneManager.LoadScene("Game");
        //    SceneManager.sceneLoaded += OnSceneLoad;
        //    Debug.Log("Loading game scene");
        //    StartClue.SetActive(true);

        //}
    }

    private IEnumerator promptTextShow()
    {
        yield return new WaitForSeconds(2f);
        turnOnMessage.gameObject.SetActive(false);
        promptText.gameObject.SetActive(true);
    }




    // Disable player in game scene (messes with wires)
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.PlayerCanvas.SetActive(false);
        GameManager.Instance.player.SetActive(false);
    }
    // checks if the player is in the collider for the scene and checks if the tools have been collected 
    public void OnTriggerEnter(Collider other)

    {
        Debug.Log("HEREEEE");
        if (other.CompareTag("Player"))
        {
            Player_Task5 = Player.transform.position;
            Debug.Log("HEREEEE22222");
            playerin = true;
           
        }
    }

}
