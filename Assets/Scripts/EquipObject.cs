using UnityEngine;

public class EquipObject : MonoBehaviour
{
    private bool playerIsInside = false;
    //private bool isClueObject = false; // Track if the object is a clue
    private bool isPasscodeObject = false;
    public QuantityManager quantityManager;
    // This method is called when another collider enters the trigger zone.

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Equip.performed += ctx => HandleEquip();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Equip.performed -= ctx => HandleEquip();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;

            

            // Check if this object is tagged as "Passcode"
            if (gameObject.CompareTag("Passcode"))
            {
                isPasscodeObject = true;
            }

            if (other.CompareTag("Player"))
            {
                playerIsInside = false;
                isPasscodeObject = false;
            }
        }
    }

    

    private void HandleEquip()
    {
        if (playerIsInside)
        {
            // If the object is NOT a Clue, allow deactivation with "E"
            if (!isPasscodeObject)
            {
                gameObject.SetActive(false);
            }

            // If the object is a Passcode, show the passcode message with "E"
            else
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
