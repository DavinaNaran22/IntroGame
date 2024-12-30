using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WireRepair : MonoBehaviour
{
    public GameObject Player;
    public GameObject Map;
    public GameObject ShipManager;
    public BoxCollider PART2;
    public bool playerin = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.Instance.player;
        PART2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Storage_Scene.Tools_collected == true) {

            PART2.enabled = true;
        }

        if (playerin == true && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Loading game scene");
            if (Player != null)
                Player.SetActive(false);
            if (Map != null)
                Map.SetActive(false);
            if (ShipManager != null)
                ShipManager.SetActive(false);
            SceneManager.LoadScene("Game");
        }
    }

    public void OnTriggerEnter(Collider other)

    {
        Debug.Log("HEREEEE");
        if (other.CompareTag("Player") && Storage_Scene.Tools_collected == true)
        {
            Debug.Log("HEREEEE22222");
            playerin = true;
           
        }
    }

}
