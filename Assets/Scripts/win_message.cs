using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win_message : MonoBehaviour
{
    public Canvas message;
    public SpriteRenderer Light;
    public static bool win = false;
    // Start is called before the first frame update
    
    void Start()
    {
        message.enabled = false;
    }

    // Update is called once per frame
    // when the light color turn green, go back to landscape scene
    void Update()
    {
        if (Light.color == Color.green)
        {

            message.enabled = true;
            StartCoroutine(BackToGame());

        }
    }
    // delays the scene load, updates the public static win boolean (indicates that game is finished)
    IEnumerator BackToGame() 
    {
        yield return new WaitForSeconds(3f);
        message.enabled = false;
        GameManager.Instance.task4Completed = true;
        // Reactivate Player
        SceneManager.LoadScene("landscape");
        SceneManager.sceneLoaded += OnSceneLoad;
        win = true;
    }

    // reactivating the player object/canvas
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.PlayerCanvas.SetActive(true);
        GameManager.Instance.player.SetActive(true);
    }
}