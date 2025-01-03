using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class EquipSwordOnClick : MonoBehaviour
{
    private GameObject swordPrefab; // Sword prefab to instantiate
    public Transform weaponParent; // Transform of the WeaponParent (to be rotated)
    public Button equipSwordButton;

    public GameObject equippedSword;
    public GameObject EquippedSword
    {
        get { return equippedSword; }
    }

    private bool isSwordBoosted = false; // Tracks if the sword is boosted

    public bool IsSwordBoosted
    {
        get { return isSwordBoosted; }
        set { isSwordBoosted = value; }
    }

    private bool isEquipped = false;
    private bool isSwinging = false;

    private PlayerInputActions inputActions;

    // Swinging settings
    public float swingSpeed = 2f; // How fast the swing happens
    public float swingPauseDuration = 0.2f; // Pause at the peak of the swing
    public float hitDetectionRadius = 10f; // Radius for detecting Rhino Alien hits
    public LayerMask alienLayer; // LayerMask for detecting aliens

    // Reference to other scripts
    public EquipGunOnClick gunScript;
    public EquipKnifeOnClick knifeScript;

    private Quaternion originalRotation; // Original rotation of the parent

    void Start()
    {
        swordPrefab = GameManager.Instance.SwordPrefab;
        if (equipSwordButton != null)
        {
            equipSwordButton.onClick.AddListener(ToggleSword);
        }
        else
        {
            Debug.LogError("EquipSwordButton is not assigned in the inspector.");
        }

        // Store the original rotation of the parent
        if (weaponParent != null)
        {
            originalRotation = weaponParent.localRotation;
        }
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Swing.performed += ctx => StartSwinging();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Swing.performed -= ctx => StartSwinging();
    }

    void ToggleSword()
    {
        if (!isEquipped)
        {
            EquipSword();
        }
        else
        {
            UnequipSword();
        }
    }

    void EquipSword()
    {
        // Check if the gun is equipped and unequip it
        if (gunScript != null && gunScript.IsGunEquipped)
        {
            gunScript.UnequipGun();
        }
        // Check if the knife is equipped and unequip it
        if (knifeScript != null && knifeScript.IsKnifeEquipped)
        {
            knifeScript.UnequipKnife();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Hides the cursor

        // Instantiate and attach the sword to the WeaponParent
        equippedSword = Instantiate(swordPrefab, weaponParent);
        equippedSword.SetActive(true);
        equippedSword.transform.localPosition = Vector3.zero;
        equippedSword.transform.localRotation = Quaternion.identity;

        // Reapply boost effect if sword is boosted
        if (isSwordBoosted)
        {
            ApplyBoostEffect();
        }

        isEquipped = true;
        Debug.Log("Sword equipped.");
    }


    public void UnequipSword()
    {
        if (equippedSword != null)
        {
            Destroy(equippedSword);
            equippedSword = null;
            isEquipped = false;

            // Restore the parent's original rotation
            if (weaponParent != null)
            {
                weaponParent.localRotation = originalRotation;
            }

            Debug.Log("Sword unequipped.");
        }
        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool IsSwordEquipped => isEquipped; // Public property to check if sword is equipped

    void StartSwinging()
    {
        if (isEquipped && !isSwinging)
        {
            StartCoroutine(Swing());
        }
    }

    private IEnumerator Swing()
    {
        if (weaponParent == null)
        {
            Debug.LogError("Weapon parent is not assigned! Cannot perform swing.");
            yield break;
        }

        isSwinging = true;

        // Target rotation for the swing
        Quaternion targetRotation = Quaternion.Euler(18.9f, weaponParent.localRotation.eulerAngles.y, weaponParent.localRotation.eulerAngles.z);

        // Rotate to the swing position
        while (Quaternion.Angle(weaponParent.localRotation, targetRotation) > 0.1f)
        {
            weaponParent.localRotation = Quaternion.RotateTowards(weaponParent.localRotation, targetRotation, swingSpeed * 100 * Time.deltaTime);
            DetectAlienHit(); // Check for RhinoAlien hits during the swing
            yield return null;
        }

        // Pause at the peak of the swing
        yield return new WaitForSeconds(swingPauseDuration);

        // Return to the original rotation
        while (Quaternion.Angle(weaponParent.localRotation, originalRotation) > 0.1f)
        {
            weaponParent.localRotation = Quaternion.RotateTowards(weaponParent.localRotation, originalRotation, swingSpeed * 100 * Time.deltaTime);
            DetectAlienHit(); // Check for RhinoAlien hits during the return motion
            yield return null;
        }

        isSwinging = false;
    }

    public void ApplyBoostEffect()
    {
        if (equippedSword != null)
        {
            Light swordLight = equippedSword.GetComponent<Light>();
            if (swordLight == null)
            {
                swordLight = equippedSword.AddComponent<Light>();
            }
            swordLight.type = LightType.Directional; // Set the light type to Directional
            swordLight.color = Color.magenta;        // Shiny purple light
            swordLight.intensity = 1f;              // Set intensity to 1

            Debug.Log("Boost effect reapplied to the sword.");
        }
    }


    private void DetectAlienHit()
    {
        if (equippedSword == null) return;

        // Detect nearby colliders within the specified radius
        //Collider[] hits = Physics.OverlapSphere(weaponParent.position, hitDetectionRadius, alienLayer);
        Collider[] hits = Physics.OverlapSphere(equippedSword.transform.position, hitDetectionRadius);
        foreach (var hit in hits)
        {
            // Only process objects tagged as "RhinoAlien"
            if (hit.CompareTag("RhinoAlien"))
            {
                RhinoAlienBehaviour rhinoAlien = hit.GetComponent<RhinoAlienBehaviour>();
                if (rhinoAlien != null && !rhinoAlien.isInvulnerable)
                {
                    rhinoAlien.TakeDamage(); // Trigger damage logic for the rhino alien
                    Debug.Log("Rhino Alien hit!");

                }
            }
        }
    }
}
