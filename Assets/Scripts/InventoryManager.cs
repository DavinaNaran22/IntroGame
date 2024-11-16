using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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



    //void Update()
    //{
    //    // Open inventory with "I"
    //    if (Input.GetKeyDown(KeyCode.I) && !isInventoryOpen)
    //    {
    //        OpenInventory();
    //    }
    //    // Close inventory with "ESC"
    //    else if (Input.GetKeyDown(KeyCode.Escape) && isInventoryOpen)
    //    {
    //        CloseInventory();
    //    }
    //}

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
