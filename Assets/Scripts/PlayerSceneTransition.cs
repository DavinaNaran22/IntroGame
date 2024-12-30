using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneTransition: MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private Vector3 spawnPoint;
    private GameObject player;
    protected bool hasCheckCondition = false;

    // When player enters trigger switch scene (and if no check condition)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCheckCondition)
        {
            player = other.gameObject;
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
        //SceneManager.LoadScene(scene);
        //SceneManager.sceneLoaded += OnSceneLoad;
        GameManager.Instance.hoverText.text = "";
    }

    // When scene loaded, move player to spawn point
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = spawnPoint;
        // So that text from one scene doesn't carry over from another
        GameManager.Instance.hoverText.text = "";
    }
}
