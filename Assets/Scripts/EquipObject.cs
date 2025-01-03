using UnityEngine;

public class EquipObject : MonoBehaviour
{
    private bool playerIsInside = false;
    private bool isClueObject = false; // Track if the object is a clue
    private bool isPasscodeObject = false;
    public QuantityManager quantityManager;
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

            // Check if this object is tagged as "Passcode"
            if (gameObject.CompareTag("Passcode"))
            {
                isPasscodeObject = true;
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
            isPasscodeObject = false;
        }
    }

    private void Update()
    {
        if (playerIsInside)
        {
            // If the object is NOT a Clue, allow deactivation with "E"
            if (!isClueObject && !isPasscodeObject && Input.GetKeyDown(KeyCode.E))
            {
                gameObject.SetActive(false);
            }

            // If the object is a Clue, allow deactivation with "R" only
            if (isClueObject && Input.GetKeyDown(KeyCode.R))
            {
                gameObject.SetActive(false);
            }

            // If the object is a Passcode, show the passcode message with "E"
            if (isPasscodeObject && Input.GetKeyDown(KeyCode.E))
            {
                if (quantityManager != null)
                {
                    quantityManager.ShowCraftingMessage("Passcode: 2836");
                }
                else
                {
                    Debug.LogError("QuantityManager reference is missing!");
                }
            }
        }
    }
}
