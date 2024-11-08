using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipGun : MonoBehaviour
{
    public GameObject gun;
    public Transform WeaponParent;
    public bool isEquipped = false;

    void Start()
    {
        gun.GetComponent<Rigidbody>().isKinematic = true;
    }

    
    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.R))
    //    {
    //        Drop();
    //    }
    //}

    //void Drop()
    //{
    //    WeaponParent.DetachChildren();
    //    gun.transform.eulerAngles = new Vector3(gun.transform.position.x, gun.transform.position.z, gun.transform.position.y);
    //    gun.GetComponent<Rigidbody>().isKinematic = false;
    //    gun.GetComponent<MeshCollider>().enabled = true;
    //}

    void Equip()
    {
        isEquipped = true;
        gun.GetComponent<Rigidbody>().isKinematic = true;
        gun.transform.position = WeaponParent.transform.position;
        gun.transform.rotation = WeaponParent.transform.rotation;
        gun.GetComponent<MeshCollider>().enabled = false;
        gun.transform.SetParent(WeaponParent);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                Equip();
            }
        }
    }

}
