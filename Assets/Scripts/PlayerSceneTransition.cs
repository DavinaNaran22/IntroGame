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
            SceneManager.LoadScene(scene);
            SceneManager.sceneLoaded += OnSceneLoad;
        }
    }

    protected void LoadOtherScene(GameObject Player)
    {
        player = Player;
        SceneManager.LoadScene(scene);
        SceneManager.sceneLoaded += OnSceneLoad;
        GameManager.Instance.hoverText.text = "";
    }

    // When scene loaded, move player to spawn point
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = spawnPoint;
        player.transform.rotation = (Quaternion.Euler(0, 0, 0));
        // So that text from one scene doesn't carry over from another
        GameManager.Instance.hoverText.text = "";
    }
}
