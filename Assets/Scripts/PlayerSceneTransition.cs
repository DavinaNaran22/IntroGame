using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneTransition: MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private Vector3 spawnPoint;
    private GameObject player;
    private InstantiateParts PartsManager;
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
    }

    // When scene loaded, move player to spawn point and spawn boxes
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        PartsManager = GameObject.FindWithTag("ShipPartManager").GetComponent<InstantiateParts>();
        PartsManager.CanSpawn();
        player.transform.position = spawnPoint;
    }
}
