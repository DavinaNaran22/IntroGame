using System.Collections;
using UnityEngine;

public class GreenAlienBehavior : MonoBehaviour
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
        }

        // Shoots at player if nearby and in idle pose with a gun state
        if (playerNearby && animator.GetCurrentAnimatorStateInfo(0).IsName("idle pose with a gun"))
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
        //animator.SetTrigger("HitL");
        StartCoroutine(HandleHitSequence());
    }

    // Sequence of states when alien gets hit
    private IEnumerator HandleHitSequence()
    {
        animator.SetTrigger("HitL");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("HitR");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("BackIdle");
        isHit = false;
    }


    // Sequence of states for alien
    private IEnumerator ExecuteEscapeSequence()
    {
        Debug.Log("Triggering Flight");
        // Flight animation for 10 seconds
        animator.SetTrigger("Flight");
        yield return new WaitForSeconds(10f);

        // Transition to "GetGun"
        animator.SetTrigger("GetGun");
        yield return new WaitForSeconds(0);

        // Transition to "Shot" after 10 seconds
        animator.SetTrigger("Shot");
        yield return new WaitForSeconds(2f);

        // Continue with remaining states in sequence
        animator.SetTrigger("IdleWithGun");
        //yield return new WaitForSeconds(6f);
        while (playerEquipment != null && !playerEquipment.IsWeaponEquipped())
        {
            yield return null;
        }
        while (!isHit)
        {
            yield return null;
        }

        animator.SetTrigger("BackIdle");

        //animator.SetTrigger("HitL");
        //yield return new WaitForSeconds(1f);

        //animator.SetTrigger("HitR");
        //yield return new WaitForSeconds(1f);

        // Set the Dead state and update isDead flag
        //animator.SetTrigger("Dead");
        //isDead = true; // Mark the alien as dead
    }
}
