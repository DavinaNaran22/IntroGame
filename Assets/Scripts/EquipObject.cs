using UnityEngine;

public class EquipObject : MonoBehaviour
{
    private bool playerIsInside = false;
    private bool isClueObject = false; // Track if the object is a clue

    // This method is called when another collider enters the trigger zone.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;

            // Check if this object is tagged as "Clue"
            if (gameObject.CompareTag("Clue"))
            {
                isClueObject = true;
            }
        }
    }

    // This method is called when another collider leaves the trigger zone.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            isClueObject = false; // Reset clue status when player leaves
        }
    }

    private void Update()
    {
        if (playerIsInside)
        {
            // If the object is NOT a Clue, allow deactivation with "E"
            if (!isClueObject && Input.GetKeyDown(KeyCode.E))
            {
                gameObject.SetActive(false);
            }

            // If the object is a Clue, allow deactivation with "R" only
            if (isClueObject && Input.GetKeyDown(KeyCode.R))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
