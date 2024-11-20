using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EquipGun : MonoBehaviour
{
    public GameObject gun;
    public Transform WeaponParent;
    public bool isEquipped = false;
    public PlayerEquipment playerEquipment;
    private PlayerInputActions inputActions;
    private bool playerInRange = false;

    void Start()
    {
        gun.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Equip.performed += ctx => Equip();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Equip.performed -= ctx => Equip();
    }


    void Equip()
    {
        if (playerInRange)
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (inputActions.Player.Equip.ReadValue<float>() > 0)
            {
                Equip();
            }

            // Automatically find PlayerEquipment on the player
            PlayerEquipment playerEquip = other.GetComponent<PlayerEquipment>();
            if (playerEquip != null)
            {
                playerEquip.EquipGun();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
