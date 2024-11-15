using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager : MonoBehaviour
{
    public GameObject player;
    public GameObject UIManager;
    public GameObject SpaceshipPart;

    // Make spawnPoint appear in editor w/o making it public
    [SerializeField] private Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        // Only instantiate player and uimanager if they don't already exist
        // Might exist if player moves from another scene into to landscape scene
        if (GameObject.FindWithTag("Player") == null && GameObject.FindWithTag("UIManager") == null)
        {
            GameObject playerGO = Instantiate(player, spawnPoint, Quaternion.identity);
            playerGO.name = "Player";
            GameObject uiGO = Instantiate(UIManager, Vector3.zero, Quaternion.identity);
            uiGO.name = "UIManager";

            // This position is just to test instantiating the spaceship parts
            Instantiate(SpaceshipPart, new Vector3(spawnPoint.x + 5f, spawnPoint.y, spawnPoint.z), Quaternion.identity);
        }
    }
}
