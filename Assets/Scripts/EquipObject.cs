using UnityEngine;

public class EquipObject : MonoBehaviour
{
    private bool playerIsInside = false;
    private bool isPasscodeObject = false;
    public QuantityManager quantityManager;
    private EquipManager equipManager;
    private EquipData objectData;

    // Input system
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

    private void Start()
    {
        equipManager = GameManager.Instance.equipManager;
        objectData = equipManager.GetFromEquipList(gameObject);
        if (objectData != null)
        {
            // Deactivate if already been equipped
            if (objectData.hasBeenEquiped)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            objectData = new EquipData(gameObject, false);
            equipManager.equipObjects.Add(objectData);
        }
    }

    // This method is called when another collider enters the trigger zone.
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
        }
    }

    // This method is called when another collider leaves the trigger zone.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            isPasscodeObject = false;
        }
    }

    private void HandleEquip()
    {
        if (playerIsInside)
        {
            // If the object is NOT a Passcode, allow deactivation
            if (!isPasscodeObject)
            {
                objectData.hasBeenEquiped = true;
                gameObject.SetActive(false);
            }
            // If the object is a Passcode, show the passcode message
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
