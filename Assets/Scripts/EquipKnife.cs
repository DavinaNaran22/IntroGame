using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipKnife : MonoBehaviour
{
    public GameObject knife;
    public Transform WeaponParent;
    public PlayerEquipment playerEquipment;

    public float stabDistance = 4f;  // How far the knife will move forward
    public float stabSpeed = 2f;      // How fast the knife will move
    private Vector3 originalPosition;
    private bool isStabbing = false;
    private bool isEquipped = false;

    void Start()
    {
        knife.GetComponent<Rigidbody>().isKinematic = true;
        originalPosition = knife.transform.localPosition;
    }

    void Update()
    {
        // Stabbing action
        if (Input.GetMouseButton(0) && isEquipped && !isStabbing)
        {
            StartCoroutine(Stab());
        }
    }


    void Equip()
    {
        // Attach knife to weapon parent
        knife.GetComponent<Rigidbody>().isKinematic = true;
        knife.transform.position = WeaponParent.transform.position;
        knife.transform.rotation = WeaponParent.transform.rotation;
        knife.GetComponent<MeshCollider>().enabled = false;
        knife.transform.SetParent(WeaponParent);
        originalPosition = knife.transform.localPosition;
        isEquipped = true;

        if (playerEquipment != null)
        {
            playerEquipment.EquipKnife();
        }
    }

    private IEnumerator Stab()
    {
        isStabbing = true;

        // Move knife forward
        Vector3 targetPosition = originalPosition + knife.transform.forward * stabDistance;
        while (Vector3.Distance(knife.transform.localPosition, targetPosition) > 0.01f)
        {
            knife.transform.localPosition = Vector3.MoveTowards(knife.transform.localPosition, targetPosition, stabSpeed * Time.deltaTime);
            yield return null;
        }

        // Retract knife back to original position
        while (Vector3.Distance(knife.transform.localPosition, originalPosition) > 0.01f)
        {
            knife.transform.localPosition = Vector3.MoveTowards(knife.transform.localPosition, originalPosition, stabSpeed * Time.deltaTime);
            yield return null;
        }

        isStabbing = false;
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
