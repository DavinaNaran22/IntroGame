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
    //public GameObject showClue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { // if the puzzle is  complete show the clue to pickup, deactivte previous scene/ activate next
        if (Puzzle.Puzzle_Complete == true)
        {
            ClueTask.SetActive(false);
            MessagePuzzle.SetActive(false);
            repair4.SetActive(false);
            caveEntrance.SetActive(true);
            GameManager.Instance.ShowClueScript.enabled = true;

        }
        // when the puzzle is not complete and the key r is pressed/ last clue collected load the puzzle scene
        if (Input.GetKeyDown(KeyCode.R) && Puzzle.Puzzle_Complete == false && Task4Clue.Clue_Collected == true)
        {
            Debug.Log("PUZZLE");
            MessagePuzzle.SetActive(false);
            SceneManager.LoadScene("Puzzle");
            SceneManager.sceneLoaded += OnSceneLoad;
            Debug.Log("Loading puzzle scene");
          

        }
      
    }

    public void OnTriggerEnter(Collider other)
    {
        // message to direct player to piece together the clue once the last clue is collected 
        if (Task4Clue.Clue_Collected == true && Puzzle.Puzzle_Complete == false) {
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