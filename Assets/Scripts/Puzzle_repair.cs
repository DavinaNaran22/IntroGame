using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle_repair : MonoBehaviour
{
    public GameObject MessagePuzzle;
    public GameObject ClueTask;
    public GameObject repair4;
    public GameObject caveEntrance;
    private PlayerInputActions inputActions;
    public GameObject showClue;
    //public GameObject showClue;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.complTaskFive) transform.parent.gameObject.SetActive(false);
        //showClue.SetActive(GameManager.ShowClueActive);
    }
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {

        inputActions.Player.Enable();
        inputActions.Player.Repair.performed += ctx => ToggleRepair2();
    }

    private void OnDestroy()
    {
        inputActions.Player.Disable();
        inputActions.Player.Repair.performed += ctx => ToggleRepair2();
    }

    private void ToggleRepair2()
    {
        if (Puzzle.Puzzle_Complete == false && Task4Clue.Clue_Collected == true)
        {
            Debug.Log("PUZZLE");
            MessagePuzzle.SetActive(false);
            //GameManager.ShowClueActive = true;
            SceneManager.LoadScene("Puzzle");
            SceneManager.sceneLoaded += OnSceneLoad;
            Debug.Log("Loading puzzle scene");
            showClue.SetActive(true);

        }
    }
    // Update is called once per frame
    void Update()
    { // if the puzzle is  complete show the clue to pickup, deactivte previous scene/ activate next
        if (Puzzle.Puzzle_Complete == true)
        {
            GameManager.Instance.complTaskFour = true;
            ClueTask.SetActive(false);
            MessagePuzzle.SetActive(false);
            repair4.SetActive(false);
            caveEntrance.SetActive(true);

        }
        //// when the puzzle is not complete and the key r is pressed/ last clue collected load the puzzle scene
        //if (Input.GetKeyDown(KeyCode.R) && Puzzle.Puzzle_Complete == false && Task4Clue.Clue_Collected == true)
        //{
        //    Debug.Log("PUZZLE");
        //    MessagePuzzle.SetActive(false);
        //    SceneManager.LoadScene("Puzzle");
        //    SceneManager.sceneLoaded += OnSceneLoad;
        //    Debug.Log("Loading puzzle scene");
        //    showClue.SetActive(true);
        //}

    }

    public void OnTriggerEnter(Collider other)
    {
        // message to direct player to piece together the clue once the last clue is collected 
        if (Task4Clue.Clue_Collected == true && Puzzle.Puzzle_Complete == false)
        {
            Debug.Log("Here");
            MessagePuzzle.SetActive(true);
        }

    }


    // deactivates the player that messes with the mouse input in the puzzle scene
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.PlayerCanvas.SetActive(false);
        GameManager.Instance.player.SetActive(false);
    }

}