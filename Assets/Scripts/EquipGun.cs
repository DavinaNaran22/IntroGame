using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipGun : MonoBehaviour
{
    public GameObject gun;
    public Transform WeaponParent;
    public bool isEquipped = false;
    public PlayerEquipment playerEquipment;

    void Start()
    {
        gun.GetComponent<Rigidbody>().isKinematic = true;
    }


    void Equip()
    {
        // Attach gun to weapon parent
        isEquipped = true;
        gun.GetComponent<Rigidbody>().isKinematic = true;
        gun.transform.position = WeaponParent.transform.position;
        gun.transform.rotation = WeaponParent.transform.rotation;
        gun.GetComponent<MeshCollider>().enabled = false;
        gun.transform.SetParent(WeaponParent);

        if (playerEquipment != null)
        {
            playerEquipment.EquipGun();
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    // Equip gun if player is near
    //    if (other.gameObject.tag == "Player")
    //    {
    //        if(Input.GetKey(KeyCode.E))
    //        {
    //            Equip();
    //        }
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            Equip();

            // Automatically find PlayerEquipment on the player
            PlayerEquipment playerEquip = other.GetComponent<PlayerEquipment>();
            if (playerEquip != null)
            {
                playerEquip.EquipGun();
            }
        }
    }

}
