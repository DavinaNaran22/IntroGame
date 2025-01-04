using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipShovelOnClick : MonoBehaviour
{
    public GameObject shovelPrefab; // Shovel prefab to instantiate
    public Transform shovelPlacement; // Transform where the shovel will be attached
    public Button equipShovelButton; // UI button to equip the shovel
    public GameObject shovelImage; // Reference to the shovel image in the UI
    public GameObject thrusterimg;
    public string equipThrusterText = "Press E to equip thruster"; // Text for thruster interaction

    private GameObject thruster; // Reference to the thruster GameObject
    public GameObject equippedShovel; // Reference to the equipped shovel

    private bool isShovelDeactivated = false; // Flag to track if the shovel is already deactivated

    public GameObject crosshair; // Reference to the crosshair UI

    // References to other weapon scripts
    public EquipGunOnClick equipGunScript;
    public EquipKnifeOnClick equipKnifeScript;

    public EquipObject equipObject;

    void Start()
    {
        thrusterimg.SetActive(false);
        // Assign the button's click event
        if (equipShovelButton != null)
        {
            equipShovelButton.onClick.AddListener(EquipShovel);
        }
        else
        {
            Debug.LogError("EquipShovelButton is not assigned in the inspector.");
        }

        // Ensure initial states
        if (shovelImage != null)
        {
            shovelImage.SetActive(false);
        }

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is "landscape"
        if (scene.name == "landscape")
        {
            Debug.Log("Landscape scene loaded. Searching for thruster...");
            thruster = GameObject.Find("Engine1"); // Replace "Engine1" with the exact name in your hierarchy

            if (thruster != null)
            {
                Debug.Log("Thruster found successfully.");
                DisableThrusterComponents(); // Disable the thruster components initially
            }
            else
            {
                Debug.LogError("Thruster not found in the landscape scene. Please check the name.");
            }
        }
    }

    void Update()
    {
        if (thruster != null && !thruster.activeSelf && !isShovelDeactivated)
        {
            DeactivateShovelAndImage();
            isShovelDeactivated = true; // Prevent multiple calls
        }
    }

    void EquipShovel()
    {
        // Ensure other weapons are unequipped
        if (equipGunScript != null && equipGunScript.IsGunEquipped)
        {
            equipGunScript.UnequipGun();
            crosshair.SetActive(false);
        }

        if (equipKnifeScript != null && equipKnifeScript.IsKnifeEquipped)
        {
            equipKnifeScript.UnequipKnife();

            // Ensure any ongoing stab actions are stopped
            equipKnifeScript.StopAllCoroutines();
            if (equipKnifeScript.audioSource != null && equipKnifeScript.audioSource.isPlaying)
            {
                equipKnifeScript.audioSource.Stop();
            }
        }

        // Ensure the thruster is active before equipping the shovel
        if (thruster == null || !thruster.activeSelf)
        {
            Debug.LogWarning("Thruster is not active. Shovel cannot be equipped.");
            return;
        }

        if (shovelPrefab == null || shovelPlacement == null)
        {
            Debug.LogError("Shovel prefab or placement is not assigned.");
            return;
        }

        

        // Instantiate and attach the shovel prefab to the placement transform
        equippedShovel = Instantiate(shovelPrefab, shovelPlacement);
        equippedShovel.SetActive(true);

        // Reset position and rotation relative to the placement
        equippedShovel.transform.localPosition = Vector3.zero;
        equippedShovel.transform.localRotation = Quaternion.identity;

        // Activate the shovel image in the UI
        if (shovelImage != null)
        {
            shovelImage.SetActive(true);
        }

        // Enable components on the thruster after the shovel is equipped
        ActivateThrusterComponents();

       

        Debug.Log("Shovel equipped.");
        isShovelDeactivated = false;
    }

    private void ActivateThrusterComponents()
    {
        if (thruster != null)
        {
            // Enable BoxCollider
            BoxCollider collider = thruster.GetComponent<BoxCollider>();
            if (collider == null)
            {
                collider = thruster.AddComponent<BoxCollider>();
            }
            collider.size = new Vector3(6.2f, 5.27f, 6.79f);
            collider.isTrigger = true;
            collider.enabled = true;

            // Enable PlayerNearText script
            PlayerNearText playerNearText = thruster.GetComponent<PlayerNearText>();
            if (playerNearText == null)
            {
                playerNearText = thruster.AddComponent<PlayerNearText>();
            }
            playerNearText.Text = equipThrusterText;

            // Enable EquipObject script
            equipObject = thruster.GetComponent<EquipObject>();
            if (equipObject == null)
            {
                thruster.AddComponent<EquipObject>();
                
            }
            else
            {
                equipObject.enabled = true;
            }
            

            Debug.Log("Thruster components activated after equipping the shovel.");
        }
    }


    private void DisableThrusterComponents()
    {
        if (thruster != null)
        {
            // Disable the BoxCollider
            BoxCollider collider = thruster.GetComponent<BoxCollider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Disable the PlayerNearText script
            PlayerNearText playerNearText = thruster.GetComponent<PlayerNearText>();
            if (playerNearText != null)
            {
                playerNearText.enabled = false;
            }

            // Disable the EquipObject script
            EquipObject equipObject = thruster.GetComponent<EquipObject>();
            if (equipObject != null)
            {
                equipObject.enabled = false;
            }

            Debug.Log("Thruster components disabled.");
        }
    }

    

    private void DeactivateShovelAndImage()
    {
        // Destroy the equipped shovel
        if (equippedShovel != null)
        {
            Destroy(equippedShovel);
            equippedShovel = null;
        }

        // Deactivate the shovel image
        if (shovelImage != null)
        {
            shovelImage.SetActive(false);
        }

        

        thrusterimg.SetActive(true);

        Debug.Log("Shovel and shovel image deactivated.");
    }
}
