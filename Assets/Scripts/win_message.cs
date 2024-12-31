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
    void Update()
    {
        if (Light.color == Color.green)
        {

            message.enabled = true;
            StartCoroutine(BackToGame());

        }
    }

    IEnumerator BackToGame() 
    {
        yield return new WaitForSeconds(3f);
        message.enabled = false;
        // Reactivate Player
        SceneManager.LoadScene("landscape");
        SceneManager.sceneLoaded += OnSceneLoad;
        win = true;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.player.SetActive(true);
    }
}