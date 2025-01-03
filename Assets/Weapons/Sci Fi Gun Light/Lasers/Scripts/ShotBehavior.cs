//https://www.youtube.com/watch?v=Yl9MhhoBkFU

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
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
        }

    }

    public void setTarget(Vector3 target)
    {
        m_target = target;
    }




}
