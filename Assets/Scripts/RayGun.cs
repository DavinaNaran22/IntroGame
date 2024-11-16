//https://www.youtube.com/watch?v=1rv8lv_TOc8

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayGun : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;
    public EquipGun equipGun;

    RaycastHit hit;
    float range = 1000.0f;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Stab.performed += ctx => Shoot();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Stab.performed -= ctx => Shoot();
    }


    //void Update()
    //{
    //    // Shoots if gun is equipped
    //    if (equipGun.isEquipped && equipGun != null)
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            if (Time.time > m_shootRateTimeStamp)
    //            {
    //                shootRay();
    //                m_shootRateTimeStamp = Time.time + shootRate;
    //            }
    //        }
    //    }
    //}

    void Shoot()
    {
        if (equipGun.isEquipped && equipGun != null)
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                shootRay();
                m_shootRateTimeStamp = Time.time + shootRate;
            }
        }
    }


    void shootRay()
    {
        // Shoots a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 2f);

            if (hit.collider.CompareTag("Alien"))
            {
                GreenAlienBehavior alien = hit.collider.GetComponent<GreenAlienBehavior>();
                if (alien != null)
                {
                    alien.TakeDamage();
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
