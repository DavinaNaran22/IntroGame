using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuantityManager : MonoBehaviour
{
    [Header("Single Items")]
    public GameObject knife;
    public GameObject gun;
    public GameObject sword;
    public GameObject alienAlloy;
    public GameObject thruster;
    public GameObject thermalConductor;
    public GameObject toolbox;

    [Header("Single Item UI Elements")]
    public GameObject knifeImage;
    public GameObject gunImage;
    public GameObject swordImage;
    public GameObject alienAlloyImage;
    public GameObject thrusterImage;
    public GameObject thermalConductorImage;
    public GameObject toolboxImage;

    [Header("Collectible Items")]
    public List<GameObject> medicines;
    public List<GameObject> bullets;
    public List<GameObject> herbs;
    public List<GameObject> metalsDropped;
    public List<GameObject> wood;

    [Header("Collectible Item UI Elements")]
    public TextMeshProUGUI medicineText;
    public TextMeshProUGUI bulletsText;
    public TextMeshProUGUI herbsText;
    public TextMeshProUGUI metalsText;
    public TextMeshProUGUI woodText;

    private int medicineCount = 0;
    private int bulletsCount = 0;
    private int herbsCount = 0;
    private int metalsCount = 0;
    private int woodCount = 0;

    private bool wasKnifeActive;
    private bool wasGunActive;
    private bool wasSwordActive;
    private bool wasAlienAlloyActive;
    private bool wasThrusterActive;
    private bool wasThermalConductorActive;
    private bool wasToolboxActive;

    private Dictionary<GameObject, bool> medicineStates = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, bool> bulletsStates = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, bool> herbsStates = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, bool> metalsStates = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, bool> woodStates = new Dictionary<GameObject, bool>();

    private void Start()
    {
        // Initialize single item UI
        SetActive(knifeImage, false);
        SetActive(gunImage, false);
        SetActive(swordImage, false);
        SetActive(alienAlloyImage, false);
        SetActive(thrusterImage, false);
        SetActive(thermalConductorImage, false);
        SetActive(toolboxImage, false);

        // Initialize collectible item text
        UpdateText(medicineText, "Medicine", medicineCount);
        UpdateText(bulletsText, "Bullets", bulletsCount);
        UpdateText(herbsText, "Herbs", herbsCount);
        UpdateText(metalsText, "Metal", metalsCount);
        UpdateText(woodText, "Wood", woodCount);

        // Store initial states for single items
        wasKnifeActive = knife != null && knife.activeInHierarchy;
        wasGunActive = gun != null && gun.activeInHierarchy;
        wasSwordActive = sword != null && sword.activeInHierarchy;
        wasAlienAlloyActive = alienAlloy != null && alienAlloy.activeInHierarchy;
        wasThrusterActive = thruster != null && thruster.activeInHierarchy;
        wasThermalConductorActive = thermalConductor != null && thermalConductor.activeInHierarchy;
        wasToolboxActive = toolbox != null && toolbox.activeInHierarchy;

        // Initialize collectible states
        InitializeItemStates(medicines, medicineStates);
        InitializeItemStates(bullets, bulletsStates);
        InitializeItemStates(herbs, herbsStates);
        InitializeItemStates(metalsDropped, metalsStates);
        InitializeItemStates(wood, woodStates);
    }

    private void Update()
    {
        // Handle single items
        HandleSingleItem(knife, ref wasKnifeActive, knifeImage);
        HandleSingleItem(gun, ref wasGunActive, gunImage);
        HandleSingleItem(sword, ref wasSwordActive, swordImage);
        HandleSingleItem(alienAlloy, ref wasAlienAlloyActive, alienAlloyImage);
        HandleSingleItem(thruster, ref wasThrusterActive, thrusterImage);
        HandleSingleItem(thermalConductor, ref wasThermalConductorActive, thermalConductorImage);
        HandleSingleItem(toolbox, ref wasToolboxActive, toolboxImage);

        // Handle collectible items
        HandleCollectibleItems(medicines, medicineStates, ref medicineCount, medicineText, "Medicine");
        HandleCollectibleItems(bullets, bulletsStates, ref bulletsCount, bulletsText, "Bullets");
        HandleCollectibleItems(herbs, herbsStates, ref herbsCount, herbsText, "Herbs");
        HandleCollectibleItems(metalsDropped, metalsStates, ref metalsCount, metalsText, "Metal");
        HandleCollectibleItems(wood, woodStates, ref woodCount, woodText, "Wood");
    }

    // Helper to initialize item states
    private void InitializeItemStates(List<GameObject> items, Dictionary<GameObject, bool> states)
    {
        foreach (GameObject item in items)
        {
            if (item != null)
            {
                states[item] = item.activeInHierarchy;
            }
        }
    }

    // Handle single items (knife, gun, etc.)
    private void HandleSingleItem(GameObject item, ref bool wasActive, GameObject image)
    {
        if (item != null)
        {
            bool isActive = item.activeInHierarchy;
            if (wasActive && !isActive && image != null)
            {
                image.SetActive(true);
            }
            wasActive = isActive;
        }
    }

    // Handle collectible items (medicine, bullets, etc.)
    private void HandleCollectibleItems(List<GameObject> items, Dictionary<GameObject, bool> states, ref int count, TextMeshProUGUI text, string itemName)
    {
        foreach (GameObject item in items)
        {
            if (item != null)
            {
                bool isActive = item.activeInHierarchy;
                if (states[item] && !isActive)
                {
                    count++;
                    UpdateText(text, itemName, count);
                }
                states[item] = isActive;
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
        if (obj != null)
        {
            obj.SetActive(state);
        }
    }
}
