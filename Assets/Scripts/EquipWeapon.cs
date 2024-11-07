using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipWeapon : MonoBehaviour
{
    public GameObject knife;
    public Transform WeaponParent;

    void Start()
    {
        knife.GetComponent<Rigidbody>().isKinematic = true;
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Drop();
        }
    }

    void Drop()
    {
        WeaponParent.DetachChildren();
        knife.transform.eulerAngles = new Vector3(knife.transform.position.x, knife.transform.position.z, knife.transform.position.y);
        knife.GetComponent<Rigidbody>().isKinematic = false;
        knife.GetComponent<MeshCollider>().enabled = true;
    }

    void Equip()
    {
        knife.GetComponent<Rigidbody>().isKinematic = true;
        knife.transform.position = WeaponParent.transform.position;
        knife.transform.rotation = WeaponParent.transform.rotation;
        knife.GetComponent<MeshCollider>().enabled = false;
        knife.transform.SetParent(WeaponParent);

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
