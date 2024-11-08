using System.Collections;
using UnityEngine;

public class GreenAlienBehavior : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public Animator animator;

    private bool playerNearby = false;

    private void Start()
    {
        // Start with Idle animation
        animator.SetTrigger("Idle");
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius && !playerNearby)
        {
            // Trigger flight when player enters detection radius
            playerNearby = true;
            StartCoroutine(ExecuteEscapeSequence());
        }
    }

    private IEnumerator ExecuteEscapeSequence()
    {
        // Flight animation for 10 seconds
        animator.SetTrigger("Flight");
        yield return new WaitForSeconds(10f);

        // Transition to "GetGun" after 0 seconds
        animator.SetTrigger("GetGun");
        yield return new WaitForSeconds(0);

        // Transition to "Shot" after 10 seconds
        animator.SetTrigger("Shot");
        yield return new WaitForSeconds(2f);  // Adjust this duration as necessary

        // Continue with remaining states in sequence
        animator.SetTrigger("IdleWithGun");
        yield return new WaitForSeconds(2f);

        animator.SetTrigger("HitL");
        yield return new WaitForSeconds(1f);

        animator.SetTrigger("HitR");
        yield return new WaitForSeconds(1f);

        animator.SetTrigger("Dead");
    }
}
