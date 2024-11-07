using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKnife : MonoBehaviour
{

    public GameObject knifeOnPlayer;


    void Start()
    {
        knifeOnPlayer.SetActive(false);
    }

   private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.G))
            {
                this.gameObject.SetActive(false);
                knifeOnPlayer.SetActive(true);
            }
        }
    }

}
   