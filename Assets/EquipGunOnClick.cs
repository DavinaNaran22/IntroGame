using UnityEngine;
using UnityEngine.UI;

public class EquipGunOnClick : MonoBehaviour
{
    // References to the gun and the player's hand
    public GameObject gunPrefab; // Gun prefab to instantiate
    public Transform playerHand; // Transform of the player's hand

    // Button to trigger the action
    public Button equipGunButton;

    // Reference to the instantiated gun
    private GameObject equippedGun;

    public bool IsGunEquipped => equippedGun != null; // Check if the gun is equipped

    void Start()
    {
        // Ensure the button is assigned and add a listener
        if (equipGunButton != null)
        {
            equipGunButton.onClick.AddListener(EquipGun);
        }
        else
        {
            Debug.LogError("EquipGunButton is not assigned in the inspector.");
        }
    }

    void EquipGun()
    {
        if (equippedGun == null)
        {
            // Instantiate the gun and attach it to the player's hand
            equippedGun = Instantiate(gunPrefab, playerHand);

            // Ensure the gun is active
            equippedGun.SetActive(true);

            // Adjust position and rotation to align with the hand
            equippedGun.transform.localPosition = Vector3.zero; // Adjust as needed
            equippedGun.transform.localRotation = Quaternion.identity; // Adjust as needed

            Debug.Log("Gun equipped and activated.");
        }
        else
        {
            Debug.LogWarning("Gun is already equipped.");
        }
    }

}
