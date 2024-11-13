using System.Collections;
using UnityEngine;

public class GreenAlienBehavior : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public Animator animator;

    private bool playerNearby = false;
    private bool isDead = false; // Flag to check if the alien is dead

    private void Start()
    {
        // Directly play the Idle animation for 5 seconds

        StartCoroutine(WaitBeforeEscapeSequence()); // Wait for 5 seconds before starting the sequence
    }

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
    }

    private IEnumerator WaitBeforeEscapeSequence()
    {
        // Wait for 5 seconds before starting the escape sequence
        yield return new WaitForSeconds(5f);
        StartCoroutine(ExecuteEscapeSequence());
    }

    private IEnumerator ExecuteEscapeSequence()
    {
        Debug.Log("Triggering Flight");
        // Flight animation for 10 seconds
        animator.SetTrigger("Flight");
        yield return new WaitForSeconds(10f); // Duration for Flight

        // Transition to "GetGun"
        animator.SetTrigger("GetGun");
        yield return new WaitForSeconds(0);

        // Transition to "Shot" after 10 seconds
        animator.SetTrigger("Shot");
        yield return new WaitForSeconds(2f); // Adjust this duration as necessary

        // Continue with remaining states in sequence
        animator.SetTrigger("IdleWithGun");
        yield return new WaitForSeconds(6f);

        animator.SetTrigger("HitL");
        yield return new WaitForSeconds(1f);

        animator.SetTrigger("HitR");
        yield return new WaitForSeconds(1f);

        // Set the Dead state and update isDead flag
        animator.SetTrigger("Dead");
        isDead = true; // Mark the alien as dead
    }
}