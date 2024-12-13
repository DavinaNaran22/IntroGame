using UnityEngine;

public class EquipObject : MonoBehaviour
{
    private bool playerIsInside = false;

    // This method is called when another collider enters the trigger zone.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the player (by tag or other identification method)
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
        }
    }

    // This method is called when another collider leaves the trigger zone.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
        }
    }

    private void Update()
    {
        // Check if the player is in range AND the player presses "E"
        if (playerIsInside && Input.GetKeyDown(KeyCode.E))
        {
            // Deactivate this object
            gameObject.SetActive(false);
        }
    }
}
