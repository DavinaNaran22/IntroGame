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

    // Load the game scene for the player to repair the wires
    private void ToggleRepair()
    {
        if (GameManager.Instance.completedTaskFour) return;
        if (playerin == true && wing_attached.WingTask == true)
        {
            GameManager.StartClueActive = true;
            // Unlock cursor (e.g. if they were holding knife before)
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Game");
            SceneManager.sceneLoaded += OnSceneLoad;
            Debug.Log("Loading game scene");
            //StartClue.SetActive(true);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.Instance.player;
        StartClue.SetActive(GameManager.StartClueActive);
        stopWingAttached.SetActive(false);
        PART2.enabled = false;
        if (!GameManager.Instance.completedTaskFour) turnOnMessage.gameObject.SetActive(true);
        StartCoroutine(PromptTextShow());
    }


    private IEnumerator PromptTextShow()
    {
        yield return new WaitForSeconds(2f);
        if (!GameManager.Instance.completedTaskFour) turnOnMessage.gameObject.SetActive(false);
        promptText.gameObject.SetActive(true);
    }

    // Disable player in game scene (messes with wires)
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.PlayerCanvas.SetActive(false);
        GameManager.Instance.player.SetActive(false);
    }

    // Checks if the player is in the collider for the scene and checks if the tools have been collected 
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
