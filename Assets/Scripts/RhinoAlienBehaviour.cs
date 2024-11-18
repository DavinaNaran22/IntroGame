using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoAlienBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform shootingPoint;
    public float detectionRadius = 5f;
    public Animator animator;
    public PlayerEquipment playerEquipment;

    public GameObject shotPrefab;
    public float shootRate = 0.5f;
    private float m_shootRateTimeStamp;

    private bool playerNearby = false;
    private bool isDead = false; // Flag to check if the alien is dead
    private bool isHit = false;

    private void Update()
    {
        if (isDead) return; // Stop any further updates if the alien is dead

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius && !playerNearby)
        {
            // Trigger flight when player enters detection radius
            playerNearby = true;
            StartCoroutine(ExecuteEscapeSequence());
        }
        else if (distanceToPlayer > detectionRadius && playerNearby)
        {
            // Reset playerNearby flag when player exits detection radius
            playerNearby = false;
            animator.SetTrigger("BackWalk");
        }

        // Shoots at player if nearby and in idle pose with a gun state
        if (playerNearby && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack(3)"))
        {
            AutoShootAtPlayer();
        }
    }

    // Alien shoots at the player automatically
    private void AutoShootAtPlayer()
    {
        if (Time.time > m_shootRateTimeStamp)
        {
            ShootLaserAtPlayer();
            m_shootRateTimeStamp = Time.time + shootRate;
        }
    }

    // Alien shoots a laser at the player
    private void ShootLaserAtPlayer()
    {
        if (shootingPoint == null)
        {
            Debug.LogError("Shooting point is not set for the alien.");
            return;
        }

        GameObject laser = GameObject.Instantiate(shotPrefab, shootingPoint.position, Quaternion.identity);
        Vector3 targetPosition = player.position;

        // Aim the laser at the player
        laser.transform.LookAt(targetPosition);

        // Set laser's target position and make it move
        laser.GetComponent<ShotBehavior>().setTarget(targetPosition);

        // Destroy laser after a set time (e.g., 2 seconds)
        Destroy(laser, 2f);
    }

    public void TakeDamage()
    {
        if (isHit) return; // Prevent multiple hits
        isHit = true;
        StartCoroutine(HandleHitSequence());
    }

    // Sequence of states when alien gets hit
    private IEnumerator HandleHitSequence()
    {
        animator.SetTrigger("GetHit");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("BackShout");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Attack3");
        yield return new WaitForSeconds(1f);
        isHit = false;
    }


    //// Sequence of states for alien
    private IEnumerator ExecuteEscapeSequence()
    {
        Debug.Log("Triggering Idle");
        // Idle animation for 10 seconds
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(10f);

        // Transition to "Jump"
        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(0);

        // Transition to "Shout" after 10 seconds
        animator.SetTrigger("Shout");
        yield return new WaitForSeconds(1f);

        // Continue with remaining states in sequence
        animator.SetTrigger("Attack3");
        yield return new WaitForSeconds(6f);
        while (playerEquipment != null && !playerEquipment.IsWeaponEquipped())
        {
            yield return null;
        }
        while (!isHit)
        {
            yield return null;
        }

        //animator.SetTrigger("GetHit");
        animator.SetTrigger("BackShout");


        //    // Set the Dead state and update isDead flag
        //    //animator.SetTrigger("Dead");
        //    //isDead = true; // Mark the alien as dead
        //
    }


}
