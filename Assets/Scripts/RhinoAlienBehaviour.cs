using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoAlienBehaviour : FindPlayerTransform
{
    public Transform shootingPoint;
    public float detectionRadius = 5f;
    public Animator animator;
    public PlayerEquipment playerEquipment;
    public RhinoDamageBar damageBar;
    public GameObject Healthlimit;
    public CaveCamRestrict caveCamRestrict;
    public EnterAlienArea3 enterAlienArea3;

    public GameObject shotPrefab;
    public float shootRate = 0.5f;
    private float m_shootRateTimeStamp;

    private bool playerNearby = false;
    private bool isDead = false; // Flag to check if the alien is dead
    private bool isHit = false;
    private bool isInvulnerable = true; // Checks if alien is in fight mode before player can kill it

    private void Start()
    {
        Healthlimit = GameObject.FindWithTag("HealthLimit");
    }

    private void Update()
    {
        base.GetPlayerTransform();
        if (isDead) return; // Stop any further updates if the alien is dead

        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

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
        Vector3 targetPosition = Player.position;

        // Aim the laser at the player
        laser.transform.LookAt(targetPosition);

        // Set laser's target position and make it move
        laser.GetComponent<ShotBehavior>().setTarget(targetPosition);

        // Damages player when alien fires lasers
        PlayerHealth playerHealth = Healthlimit.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(0.03f); // Adjust damage percentage as needed
            Debug.Log("Alien shot the player! Dealing damage.");
        }

        // Destroy laser after a set time (e.g., 2 seconds)
        Destroy(laser, 2f);
    }

    public void TakeDamage()
    {
        if (isHit || isInvulnerable) return; // Prevent multiple hits
        isHit = true;

        // Reduce health bar
        if (damageBar != null)
        {
            damageBar.TakeDamage(0.1f); // reduce 10% health
        }
        else
        {
            Debug.LogWarning("Damage bar not assigned!");
        }

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
        Debug.Log("Rhino Alien meets player");
        enterAlienArea3.isPlayerNearby = true;
        enterAlienArea3.StartAdditionalDialogues();


        Debug.Log("Triggering Idle");
        isInvulnerable = true;
        // Idle animation for 10 seconds
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(0);

        while (enterAlienArea3 != null && enterAlienArea3.IsDialogueActive())
        {
            yield return null;
        }

        // Transition to "Jump"
        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(0);

        while (enterAlienArea3 != null && enterAlienArea3.IsDialogueActive())
        {
            yield return null;
        }

        // Transition to "Shout" after 10 seconds
        animator.SetTrigger("Shout");
        yield return new WaitForSeconds(1f);

        while (enterAlienArea3 != null && enterAlienArea3.IsDialogueActive())
        {
            yield return null;
        }

        // Continue with remaining states in sequence
        animator.SetTrigger("Attack3");
        yield return new WaitForSeconds(6f);
        isInvulnerable = false;
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
    }

    private void OnEnable()
    {
        if (damageBar != null)
        {
            damageBar.OnAlienDied += HandleAlienDeath; // Subscribe to event
        }
    }

    private void OnDisable()
    {
        if (damageBar != null)
        {
            damageBar.OnAlienDied -= HandleAlienDeath; // Unsubscribe from event
        }
    }

    private void HandleAlienDeath()
    {
        if (isDead) return; // Prevent duplicate calls
        isDead = true;
        animator.SetTrigger("Dead"); // Trigger "Dead" animation
        Debug.Log("Alien has died!");
        StartCoroutine(DelayedBlockAppearance());
    }

    private IEnumerator DelayedBlockAppearance()
    {
        yield return new WaitForSeconds(7f); // Wait for 7 seconds
        gameObject.SetActive(false); // Deactivate the alien GameObject
        Debug.Log("Alien is now inactive and removed from the scene.");

        // Show next scene
        caveCamRestrict.gameObject.SetActive(false);
    }

}

