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
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Storage_Scene.Tools_collected== true) {

            this.gameObject.SetActive(true);

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Storage_Scene.Tools_collected == true)
        {
            if (Input.GetKeyDown(KeyCode.R)) {

                SceneManager.LoadScene("Game");
                Player.SetActive(false);
                Map.SetActive(false);
                ShipManager.SetActive(false);
            }
           
        }
    }

}
