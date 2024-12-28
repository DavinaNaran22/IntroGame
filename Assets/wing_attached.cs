using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wing_attached : MonoBehaviour
{
    public GameObject Wing;
    public GameObject Cube;
    //public GameObject Player;
    //public GameObject Map;
    //public GameObject ShipManager;
    public static Canvas Message1;
    public static bool WingTask = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Message1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        WingTask = true;
        Wing.SetActive(false);
        Cube.SetActive(false);
        Message1.enabled = true;
       
        //SceneManager.LoadScene("Game");
        //Player.SetActive(false);
        //Map.SetActive(false);
        //ShipManager.SetActive(false);


    }




}
