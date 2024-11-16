//https://www.youtube.com/watch?v=1rv8lv_TOc8

using System.Collections;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;
    public EquipGun equipGun;

    RaycastHit hit;
    float range = 1000.0f;


    void Update()
    {
        // Shoots if gun is equipped
        if (equipGun.isEquipped && equipGun != null)
        {
            if (Input.GetMouseButton(0))
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
        // Shoots a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        }

    }



}