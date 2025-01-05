using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryCanvas; // Reference to the inventory canvas
    public GameObject inventoryTab; // Reference to the Inventory Tab
    public GameObject logsTab; // Reference to the Logs Tab
    public Button closeButton; // Reference to the close "X" button
    public Button inventoryTabButton; // Reference to the Inventory tab button
    public Button logsTabButton; // Reference to the Logs tab button

    private bool isInventoryOpen = false;
    private PlayerInputActions inputActions;
    public EquipGunOnClick gunScript;
    public EquipKnifeOnClick knifeScript;
    public EquipSwordOnClick swordScript;

    void Start()
    {
        // Ensure the canvas starts disabled
        inventoryCanvas.SetActive(false);

        // Set up button listeners
        closeButton.onClick.AddListener(CloseInventory);
        inventoryTabButton.onClick.AddListener(() => ToggleTab("Inventory"));
        logsTabButton.onClick.AddListener(() => ToggleTab("Logs"));

        // Default to the Inventory tab
        ToggleTab("Inventory");
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Inventory.performed += ctx => OpenInventory();  // Listen for Inventory action
        inputActions.Player.CloseInventory.performed += ctx => CloseInventory();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Inventory.performed -= ctx => OpenInventory();  // Listen for Inventory action
        inputActions.Player.CloseInventory.performed -= ctx => CloseInventory();
    }



    void Update()
    {
        if (inventoryCanvas.activeSelf || GameObject.FindWithTag("Keypad") != null && GameObject.FindWithTag("Keypad").activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // Need cursor visible for keypad
        else if (!inventoryCanvas.activeSelf && gunScript.IsGunEquipped)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!inventoryCanvas.activeSelf && knifeScript.IsKnifeEquipped)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!inventoryCanvas.activeSelf && swordScript.IsSwordEquipped)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }



    void OpenInventory()
    {

        if (!isInventoryOpen)
        {
            // Enable the inventory canvas and pause the game
            inventoryCanvas.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            isInventoryOpen = true;

            // Default to the Inventory tab
            ToggleTab("Inventory");
        }

    }

    void CloseInventory()
    {
        if (isInventoryOpen)
        {
            // Disable the inventory canvas and resume the game
            inventoryCanvas.SetActive(false);
            Time.timeScale = 1f; // Resume the game
            isInventoryOpen = false;
        }
    }

    void ToggleTab(string tabName)
    {
        if (tabName == "Inventory")
        {
            // Activate Inventory tab and deactivate Logs tab
            inventoryTab.SetActive(true);
            logsTab.SetActive(false);

            // Adjust button interactability (optional styling)
            inventoryTabButton.interactable = false; // Current tab is inactive for toggling
            logsTabButton.interactable = true;
        }
        else if (tabName == "Logs")
        {
            // Activate Logs tab and deactivate Inventory tab
            logsTab.SetActive(true);
            inventoryTab.SetActive(false);

            // Adjust button interactability (optional styling)
            logsTabButton.interactable = false; // Current tab is inactive for toggling
            inventoryTabButton.interactable = true;
        }
    }
}