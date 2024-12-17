using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreenNiceAlien : FindPlayerTransform
{
    public Animator animator;
    public CaveScene caveScene;
    private bool playerNearby = false;
    public float detectionRadius = 5f;
    public GameObject sword;
    public TextMeshProUGUI dialogueText;

    private void Update()
    {
        base.GetPlayerTransform();

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
            dialogueText.gameObject.SetActive(false);
        }

    }


    // Starts dialogue between player and alien
    private IEnumerator ExecuteEscapeSequence()
    {
        Debug.Log("Alien meets player");

        caveScene.isPlayerNearby = true;
        caveScene.StartAdditionalDialogues();

        // Dialogue between player and alien
        while (caveScene != null && caveScene.IsDialogueActive())
        {
            yield return null;
        }
        sword.SetActive(true);


       
    }

}
