//https://www.youtube.com/watch?v=1rv8lv_TOc8

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayGun : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;
    public EquipGunOnClick equipGunOnClick;

    RaycastHit hit;
    float range = 1000.0f;

    private PlayerInputActions inputActions;
    public InventoryManager inventoryManager; // Reference to InventoryManager


    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Start()
    {
        equipGunOnClick = GameObject.Find("InventoryManager").GetComponent<EquipGunOnClick>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

    }


    void Update()
    {
        // Prevent shooting if the inventory canvas is active
        if (inventoryManager != null && inventoryManager.inventoryCanvas.activeSelf)
        {
            return;
        }

        // Shoots if gun is equipped
        if (equipGunOnClick != null && equipGunOnClick.IsGunEquipped)
        {
            if (inputActions.Player.Shoot.ReadValue<float>() > 0)
            {
                if (Time.time > m_shootRateTimeStamp)
                {
                    shootRay();
                    m_shootRateTimeStamp = Time.time + shootRate;
                }
            }
        }

    }

    void shootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // Ray from center of screen
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1.0f);

            // Play the shooting sound
            AudioSource audioSource = laser.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }

            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 0.3f);

            if (hit.collider.CompareTag("Alien"))
            {
                GreenAlienBehavior alien = hit.collider.GetComponent<GreenAlienBehavior>();
                if (alien != null)
                {
                    alien.TakeDamage();
                }

                GAlienS2 alien2 = hit.collider.GetComponent<GAlienS2>();
                if (alien2 != null)
                {
                    alien2.TakeDamage();
                }

                GAlienS3 alien3 = hit.collider.GetComponent<GAlienS3>();
                if (alien3 != null)
                {
                    alien3.TakeDamage();
                }
            }

            if (hit.collider.CompareTag("RhinoAlien"))
            {
                RhinoAlienBehaviour alien = hit.collider.GetComponent<RhinoAlienBehaviour>();
                if (alien != null)
                {
                    alien.TakeDamage();
                }
            }
        }
    }


}


