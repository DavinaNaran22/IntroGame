using UnityEngine;
using UnityEngine.UI;

public class EquipGunOnClick : MonoBehaviour
{
    private GameObject gunPrefab; // Gun prefab to instantiate
    public Transform playerHand; // Transform of the player's hand
    public Button equipGunButton;

    private GameObject equippedGun;

    // Reference to the EquipKnifeOnClick script
    public EquipKnifeOnClick equipKnifeScript;

    void Start()
    {
        gunPrefab = GameManager.Instance.gunPrefab;
        if (equipGunButton != null)
        {
            equipGunButton.onClick.AddListener(ToggleGun);
        }
        else
        {
            Debug.LogError("EquipGunButton is not assigned in the inspector.");
        }
    }

    void ToggleGun()
    {
        if (equippedGun == null)
        {
            EquipGun();
        }
        else
        {
            UnequipGun();
        }
    }

    void EquipGun()
    {
        // Check if a knife is equipped and unequip it
        if (equipKnifeScript != null && equipKnifeScript.IsKnifeEquipped)
        {
            equipKnifeScript.UnequipKnife();
        }

        equippedGun = Instantiate(gunPrefab, playerHand);
        equippedGun.SetActive(true);
        equippedGun.transform.localPosition = Vector3.zero;
        equippedGun.transform.localRotation = Quaternion.identity;

        // Destroy the PlayerNearText script from the clone
        PlayerNearText playerNearText = equippedGun.GetComponent<PlayerNearText>();
        if (playerNearText != null)
        {
            Destroy(playerNearText);
        }


        Debug.Log("Gun equipped.");
    }

    public void UnequipGun()
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun);
            equippedGun = null;
            Debug.Log("Gun unequipped.");
        }
    }

    public bool IsGunEquipped => equippedGun != null;
}
