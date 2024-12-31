using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetPosition_T4 : MonoBehaviour
{
    [SerializeField] private string scene;
    private GameObject player;
    //private InstantiateParts PartsManager;
    protected bool hasCheckCondition = false;
    

    // When player enters trigger switch scene (and if no check condition)
    void Update()
    {
        if (win_message.win == true)
        {
            StartCoroutine(LoadAsyncScene());

            //SceneManager.LoadScene(scene);
            //SceneManager.sceneLoaded += OnSceneLoad;
        }
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        SceneManager.sceneLoaded += OnSceneLoad;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    protected void LoadOtherScene(GameObject Player)
    {
        player = Player;
        StartCoroutine(LoadAsyncScene());
        GameManager.Instance.hoverText.text = "";
    }


    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.player.transform.position = WireRepair.Player_Task5;
        
    }
}
