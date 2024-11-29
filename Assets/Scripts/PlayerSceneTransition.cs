using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneTransition: MonoBehaviour
{
    [SerializeField] protected string scene;
    [SerializeField] protected Vector3 spawnPoint;
    private GameObject player;
    private InstantiateParts PartsManager;
    // If derived class needs to check something before changing scene
    protected bool hasCheckCondition = false;

    // When player enters trigger switch scene
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCheckCondition)
        {
            LoadOtherScene(other.gameObject);
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
        //GameManager.showMinimap = SceneManager.GetActiveScene().name == "landscape";
    }
}
