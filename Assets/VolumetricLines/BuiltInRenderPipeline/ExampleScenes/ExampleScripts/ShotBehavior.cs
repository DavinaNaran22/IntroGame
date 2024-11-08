//https://www.youtube.com/watch?v=1rv8lv_TOc8

using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour
{

    public Vector3 m_target;
    public GameObject collisionExplosion;
    public float speed;

    
    void Update()
    {
        // transform.position += transform.forward * Time.deltaTime * 300f;// The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        if (m_target != null)
        {
            if (transform.position == m_target)
            {
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
        }

    }

    public void setTarget(Vector3 target)
    {
        m_target = target;
    }


    // Creates an explosion effect when the shot hits a target
    void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(
                collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }


    }

}
