using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour
{
    public GameObject target1;
    public static bool Clue_in = false;
    public static int Count_puzzle = 0;
    public static bool Puzzle_Complete  = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISON1");
        if (other.gameObject == target1)
        {
            Clue_in = true;
            Count_puzzle += 1;
            Debug.Log("COLLISON2");

        }
    }

    private void Update()
    {
        Debug.Log(Count_puzzle);
        if (Count_puzzle == 3) {
            Puzzle_Complete = true;
            //StartCoroutine(BackToGame());
        }
    }

    //IEnumerator BackToGame()
    //{
    //    yield return new WaitForSeconds(2f);
     
    //    // Reactivate Player
    //    SceneManager.LoadScene("Interior");
    //    SceneManager.sceneLoaded += OnSceneLoad;
      
    //}

    //private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    //{
    //    GameManager.Instance.PlayerCanvas.SetActive(true);
    //    GameManager.Instance.player.SetActive(true);
    //}
}