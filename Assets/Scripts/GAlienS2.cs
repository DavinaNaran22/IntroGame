using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAlienS2 : FindPlayerTransform
{
    public Transform shootingPoint;
    public float detectionRadius = 5f;
    public Animator animator;
    public PlayerEquipment playerEquipment;
    public AlienDamageBar damageBar;
    public GameObject Healthlimit;
    public float reduceHealth = 0.1f;
    public AlienRestrictScene camRestrict;
    public EnterAlienArea2 enterAlienArea2;

    public GameObject dropBlock;

    public GameObject shotPrefab;
    public float shootRate = 1f;
    private float m_shootRateTimeStamp;

    private bool playerNearby = false;
    private bool isDead = false; // Flag to check if the alien is dead
    private bool isHit = false;

    private void Start()
    {
        //GameObject uimanager = GameManager.Instance.UIManager;
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
            if (camRestrict != null && camRestrict.photoTaken)
            {
                StartCoroutine(ExecuteEscapeSequence());
            }
            else
            {
                Debug.Log("Photo has not been taken yet. Waiting...");
            }
            //StartCoroutine(ExecuteEscapeSequence());
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
        Vector3 targetPosition = Player.position;

        // Aim the laser at the player
        laser.transform.LookAt(targetPosition);

        // Set laser's target position and make it move
        laser.GetComponent<ShotBehavior>().setTarget(targetPosition);

        // Damages player when alien fires lasers
        PlayerHealth playerHealth = Healthlimit.GetComponent<PlayerHealth>();
        //PlayerHealth playerHealth = GameManager.Instance.UIManager.GetComponentInChildren<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(0.02f); // Adjust damage percentage as needed
            Debug.Log("Alien shot the player! Dealing damage.");
        }

        // Destroy laser after a set time (e.g., 2 seconds)
        Destroy(laser, 2f);
    }


    public void TakeDamage()
    {
        if (isHit) return; // Prevent multiple hits
        isHit = true;

        // Reduce health bar
        if (damageBar != null)
        {
            damageBar.TakeDamage(reduceHealth); // reduce 10% health
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
        enterAlienArea2.isPlayerNearby = true;
        enterAlienArea2.StartAdditionalDialogues();

        Debug.Log("Triggering Flight");

        // Flight animation for 0 seconds
        animator.SetTrigger("Flight");
        yield return new WaitForSeconds(0);

        // Dialogue between player and alien
        while (enterAlienArea2 != null && enterAlienArea2.IsDialogueActive())
        {
            yield return null;
        }
        enterAlienArea2.HideDialogue();

        // Transition to "GetGun"
        animator.SetTrigger("GetGun");
        yield return new WaitForSeconds(0);

        while (enterAlienArea2 != null && enterAlienArea2.IsDialogueActive())
        {
            yield return null;
        }

        // Transition to "Shot" after 10 seconds
        animator.SetTrigger("Shot");
        yield return new WaitForSeconds(2f);

        // Continue with remaining states in sequence
        animator.SetTrigger("IdleWithGun");

        while (playerEquipment != null && !playerEquipment.IsWeaponEquipped())
        {
            yield return null;
        }
        while (!isHit)
        {
            yield return null;
        }

        animator.SetTrigger("BackIdle");
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

    // Make blocks visible once alien animation is done
    private IEnumerator DelayedBlockAppearance()
    {
        yield return new WaitForSeconds(7f); // Wait for 7 seconds
        gameObject.SetActive(false); // Deactivate the alien GameObject
        Debug.Log("Alien is now inactive and removed from the scene.");

        dropBlock.SetActive(true); // Make the block visible
        Debug.Log("Drop block is now visible!");
    }
}
