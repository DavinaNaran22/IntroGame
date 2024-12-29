using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class QuantityManager : MonoBehaviour
{
    [Header("Single Items")]
    public GameObject knife;
    public GameObject gun;
    private GameObject sword;
    private GameObject alienAlloy;
    //private GameObject thruster;
    private GameObject thermalConductor;
    public GameObject toolbox;
    private GameObject shovel;
    public GameObject metalsDropped; // Changed to single item
    public GameObject woodDropped; // Changed to single item
    public GameObject stone; // Changed to single item


    [Header("Single Item UI Elements")]
    public GameObject knifeImage;
    public GameObject gunImage;
    public GameObject swordImage;
    public GameObject alienAlloyImage;
    //public GameObject thrusterImage;
    public GameObject thermalConductorImage;
    public GameObject toolboxImage;
    public GameObject shovelImage;
    public GameObject metalsDroppedImage; // Added
    public GameObject woodImage; // Added
    public GameObject stoneImage; // Changed to single item
    public GameObject clueImage;

    [Header("Collectible Items")]
    public List<GameObject> medicines;
    public List<GameObject> clues;
    [Header("Collectible Item UI Elements")]
    public TextMeshProUGUI medicineText;
    public TextMeshProUGUI herbsText;
    public TextMeshProUGUI clueText; 

    // Reference to the Crafting Message Canvas and Text
    public GameObject craftingMessageCanvas;
    public TextMeshProUGUI craftingMessageText;

    private float messageDisplayDuration = 2f; // How long to display the message

    public GameObject shovelParent;
    public GameObject swordParent;

    private int medicineCount = 0;
    private int herbsCount = 0;
    private int clueCount = 0; 

    private Dictionary<GameObject, bool> medicineStates = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, bool> herbStates = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, bool> clueStates = new Dictionary<GameObject, bool>(); // Track state of clues


    [SerializeField] PlayerHealth healthScript;


    private void Start()
    {
        // Initialize single item UI
        SetActive(knifeImage, false);
        SetActive(gunImage, false);
        SetActive(swordImage, false);
        SetActive(alienAlloyImage, false);
        //SetActive(thrusterImage, false);
        SetActive(thermalConductorImage, false);
        SetActive(toolboxImage, false);
        SetActive(shovelImage, false);
        SetActive(metalsDroppedImage, false);
        SetActive(woodImage, false);
        SetActive(craftingMessageCanvas, false);
        SetActive(shovelParent, false);
        SetActive(swordParent, false);
        SetActive(stoneImage, false);
        SetActive(clueImage, false);

        TrackAlienAlloy("RepairTask2Manager", "AlienAlloy", alienAlloyImage);

        // Initialize collectible item text
        UpdateText(medicineText, "Medicine", medicineCount);
        UpdateText(herbsText, "Herbs", herbsCount);
        UpdateText(clueText, "Clues", clueCount);
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the "Landscape" scene
        if (scene.name == "landscape")
        {
            Debug.Log($"Scene '{scene.name}' loaded. Checking item states...");

            // Check single items
            //CheckAndActivateUIElement("AlienAlloy", alienAlloyImage);
            //CheckAndActivateUIElement("thruster1", thrusterImage);
            //CheckAndActivateUIElement("ThermalConductor", thermalConductorImage);
            //CheckAndActivateUIElement("Shovel", shovelImage);
            //CheckAndActivateUIElement("MetalsDropped", metalsDroppedImage);
            //CheckAndActivateUIElement("Wood", woodImage);

            // Find metalsDropped and woodDropped
            TrackDroppedItem("RepairTask1Manager", "MetalDropped", metalsDroppedImage);
            TrackDroppedItem("RepairTask1Manager", "WoodDropped", woodImage);

            if (clueCount > 0 && clueImage != null)
            {
                SetActive(clueImage, true);
            }



            // Find and add all herbs to the list
            FindAndAddHerbsByTag();

            // Find and add all clues to the list
            FindAndAddCluesByTag(); 
        }
    }

    private void Update()
    {
        // Handle single items (like the original behavior)
        HandleSingleItem(knife, knifeImage);
        HandleSingleItem(gun, gunImage);
        HandleSingleItem(sword, swordImage);
        HandleSingleItem(toolbox, toolboxImage);

        // Handle collectible items
        HandleCollectibleItems(medicines, ref medicineCount, medicineText, "Medicine");
        HandleCollectibleItems(clues, ref clueCount, clueText, "Clues");

        // Handle herb state updates
        HandleHerbs();

        HandleClues();

        // Check if both wood and metal images are active in the UI
        if (woodImage.activeSelf && metalsDroppedImage.activeSelf)
        {
            // Activate the shovelParent when the conditions are met
            SetActive(shovelParent, true);
        }
    }


    private void TrackAlienAlloy(string parentName, string childName, GameObject alienAlloyImage)
    {
        StartCoroutine(WaitForParentAndChildState(parentName, childName, alienAlloyImage));
    }

    private System.Collections.IEnumerator WaitForParentAndChildState(string parentName, string childName, GameObject alienAlloyImage)
    {
        Debug.Log($"Waiting for parent object '{parentName}' to become available...");

        // Wait for the parent to appear in the scene
        GameObject parentObject = null;
        while (parentObject == null)
        {
            parentObject = GameObject.Find(parentName);
            yield return new WaitForSeconds(0.5f); // Avoid spamming checks too quickly
        }

        Debug.Log($"Parent object '{parentName}' found. Waiting for it to become active...");

        // Wait for the parent to become active
        yield return new WaitUntil(() => parentObject.activeInHierarchy);
        Debug.Log($"Parent object '{parentName}' is now active.");

        // Find the child object under the parent
        Transform childTransform = parentObject.transform.Find(childName);
        if (childTransform == null)
        {
            Debug.LogError($"Child object '{childName}' not found under parent '{parentName}'.");
            yield break;
        }

        GameObject childObject = childTransform.gameObject;

        // Wait for the child to become active
        yield return new WaitUntil(() => childObject.activeInHierarchy);
        Debug.Log($"Child object '{childName}' is now active.");

        // Wait for the child to become inactive
        yield return new WaitUntil(() => !childObject.activeInHierarchy);
        Debug.Log($"Child object '{childName}' is now inactive. Updating inventory UI.");

        // Update the UI for the Alien Alloy
        if (alienAlloyImage != null)
        {
            alienAlloyImage.SetActive(true);
            Debug.Log($"Alien Alloy image is now visible in the inventory.");
        }
        else
        {
            Debug.LogError("Alien Alloy image UI element is not assigned.");
        }
    }


    private void HandleSingleItem(GameObject item, GameObject image)
    {
        if (item != null && image != null)
        {
            bool isActive = item.activeInHierarchy;
            if (!isActive)
            {
                image.SetActive(true); // Show the UI element if the item is inactive
            }
            else
            {
                image.SetActive(false); // Hide the UI element if the item is active
            }
        }
    }

    private void TrackDroppedItem(string parentName, string childName, GameObject image)
    {
        GameObject parentObject = GameObject.Find(parentName); // Find parent
        if (parentObject != null)
        {
            Transform childTransform = parentObject.transform.Find(childName); // Find child by name
            if (childTransform != null)
            {
                GameObject droppedItem = childTransform.gameObject;

                StartCoroutine(WaitForItemStateChange(droppedItem, image));
            }
            else
            {
                Debug.LogWarning($"Child '{childName}' not found under parent '{parentName}'.");
            }
        }
        else
        {
            Debug.LogError($"Parent object '{parentName}' not found in the scene.");
        }
    }

    private System.Collections.IEnumerator WaitForItemStateChange(GameObject item, GameObject image)
    {
        while (true)
        {
            // Wait until the item becomes active
            yield return new WaitUntil(() => item.activeInHierarchy);

            Debug.Log($"{item.name} is now active.");

            // Wait until the item becomes inactive
            yield return new WaitUntil(() => !item.activeInHierarchy);

            Debug.Log($"{item.name} is now inactive. Adding to inventory.");

            // Display the item in the inventory UI
            if (image != null)
            {
                image.SetActive(true);
            }
        }
    }


    private void CheckAndActivateUIElement(string itemName, GameObject uiElement)
    {
        GameObject item = GameObject.Find(itemName); // Finds GameObject by name in the scene
        if (item != null)
        {
            if (!item.activeInHierarchy)
            {
                Debug.Log($"{itemName} is inactive. Activating its UI element.");
                SetActive(uiElement, true);
            }
            else
            {
                Debug.Log($"{itemName} is active. UI element remains hidden.");
            }
        }
        else
        {
            Debug.LogError($"GameObject '{itemName}' not found in the scene. Please check the scene setup.");
        }
    }

    private void FindAndAddHerbsByTag()
    {
        // Clear the list to avoid duplicate entries
        herbStates.Clear();

        // Find all objects tagged as "Herb"
        GameObject[] herbObjects = GameObject.FindGameObjectsWithTag("Herb");
        Debug.Log($"Found {herbObjects.Length} objects tagged as 'Herb'.");

        foreach (GameObject herb in herbObjects)
        {
            // Add to herbStates if not already tracked
            if (!herbStates.ContainsKey(herb))
            {
                herbStates[herb] = herb.activeInHierarchy;
                Debug.Log($"Herb added to tracking: {herb.name} (Active: {herb.activeInHierarchy})");
            }
        }
    }

    private void FindAndAddCluesByTag()
    {
        // Clear the list to avoid duplicate entries
        clueStates.Clear();

        // Find all objects tagged as "Clue"
        GameObject[] clueObjects = GameObject.FindGameObjectsWithTag("Clue");
        Debug.Log($"Found {clueObjects.Length} objects tagged as 'Clue'.");

        foreach (GameObject clue in clueObjects)
        {
            // Add to clueStates if not already tracked
            if (!clueStates.ContainsKey(clue))
            {
                clueStates[clue] = clue.activeInHierarchy;
                Debug.Log($"Clue added to tracking: {clue.name} (Active: {clue.activeInHierarchy})");
            }
        }
    }



    private void HandleHerbs()
    {
        // Create a list to track herbs that need to be processed
        List<GameObject> herbsToProcess = new List<GameObject>();

        // Iterate over the herbStates dictionary
        foreach (var herb in herbStates.Keys)
        {
            if (herb != null && !herb.activeInHierarchy && herbStates[herb])
            {
                // Add the herb to the list of herbs to process
                herbsToProcess.Add(herb);
            }
        }

        // Process the herbs outside of the iteration
        foreach (var herb in herbsToProcess)
        {
            // Increment herb count and update UI
            herbsCount++;
            UpdateText(herbsText, "Herbs", herbsCount);
            Debug.Log($"Herb count incremented. Current count: {herbsCount}");

            // Mark herb as processed
            herbStates[herb] = false;
        }
    }

    private void HandleClues()
    {
        List<GameObject> cluesToProcess = new List<GameObject>();

        foreach (var clue in clueStates.Keys)
        {
            if (clue != null && !clue.activeInHierarchy && clueStates[clue])
            {
                cluesToProcess.Add(clue);
            }
        }

        foreach (var clue in cluesToProcess)
        {
            // Increment clue count and update UI
            clueCount++;
            UpdateText(clueText, "Clues", clueCount);
            Debug.Log($"Clue count incremented. Current count: {clueCount}");

            // Mark clue as processed
            clueStates[clue] = false;

            // Show the clue image in the inventory
            if (clueImage != null)
            {
                SetActive(clueImage, true);
            }
        }
    }





    private void HandleCollectibleItems(List<GameObject> items, ref int count, TextMeshProUGUI text, string itemName)
    {
        foreach (GameObject item in items)
        {
            if (item != null)
            {
                // Initialize the item's state in the dictionary if not already present
                if (!medicineStates.ContainsKey(item))
                {
                    medicineStates[item] = item.activeInHierarchy;
                }

                // Check if the item is inactive and not already processed
                if (!item.activeInHierarchy && medicineStates[item])
                {
                    count++;
                    UpdateText(text, itemName, count);

                    // Mark the item as processed
                    medicineStates[item] = false;
                }
            }
        }
    }

    // Helper to set text
    private void UpdateText(TextMeshProUGUI text, string itemName, int count)
    {
        if (text != null)
        {
            text.text = $"{itemName}-{count}";
        }
    }

    // Helper to set active state
    private void SetActive(GameObject obj, bool state)
    {
        if (obj != null && obj.activeSelf != state)
        {
            obj.SetActive(state);
        }
    }


    // Show the crafting message
    private void ShowCraftingMessage(string message)
    {
        if (craftingMessageCanvas != null && craftingMessageText != null)
        {
            Debug.Log($"Displaying crafting message: {message}");

            // Safely activate canvas
            SetActive(craftingMessageCanvas.gameObject, true);

            // Update the crafting message text
            craftingMessageText.text = message;

            // Cancel any previously scheduled hides to avoid conflict
            CancelInvoke(nameof(HideCraftingMessage));

            // Schedule the canvas to hide after the specified duration
            Invoke(nameof(HideCraftingMessage), messageDisplayDuration);
        }
        else
        {
            Debug.LogError("Crafting message canvas or text is not assigned in the inspector.");
        }
    }


    // Hide the crafting message
    private void HideCraftingMessage()
    {
        if (craftingMessageCanvas != null)
        {
            Debug.Log("Hiding crafting message canvas.");

            // Safely deactivate canvas
            SetActive(craftingMessageCanvas.gameObject, false);
        }
        else
        {
            Debug.LogError("Crafting message canvas is not assigned in the inspector.");
        }
    }

    public void OnCraftButtonClicked(string item)
    {
        switch (item)
        {
            case "Medicine":
                CraftMedicine();
                break;
            case "Shovel":
                CraftShovel();
                break;
            // Add cases for other craftable items as needed
            default:
                ShowCraftingMessage("Invalid craft item!");
                break;
        }
    }



    // Example Crafting Method
    public void CraftMedicine()
    {
        if (herbsCount >= 2)
        {
            herbsCount -= 2;
            medicineCount++;
            UpdateText(medicineText, "Medicine", medicineCount);
            UpdateText(herbsText, "Herbs", herbsCount);
            ShowCraftingMessage("Medicine crafted successfully!");
        }
        else
        {
            ShowCraftingMessage("Not enough herbs to craft medicine!");
        }
    }

    public void CraftShovel()
    {

        // Check if both wood and metal images are active in the UI
        if (woodImage.activeSelf && metalsDroppedImage.activeSelf)
        {
            Debug.Log("Both wood and metal are available. Activating the shovel in inventory.");

            SetActive(shovelImage, true);
            // Deactivate wood and metal images since they are used in crafting
            SetActive(woodImage, false);
            SetActive(metalsDroppedImage, false);

            // Add the shovel to the inventory
            SetActive(shovelParent, false); // Show the shovel in inventory UI
            ShowCraftingMessage("Shovel crafted successfully!");
        }
        else
        {
            ShowCraftingMessage("Not enough metal or wood to craft a shovel!");
        }
    }

    // Use medicine item if player has it
    public void UseMedicine()
    {
        if (medicineCount > 0)
        {
            healthScript.Heal();
            medicineCount--;
            UpdateText(medicineText, "Medicine", medicineCount);
        }
    }
}