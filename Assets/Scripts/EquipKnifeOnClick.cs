using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class EquipKnifeOnClick : MonoBehaviour
{
    private GameObject knifePrefab; // Knife prefab to instantiate
    public Transform weaponParent; // Transform of the WeaponParent
    public Button equipKnifeButton;

    private GameObject equippedKnife;
    private bool isEquipped = false;
    private bool isStabbing = false;
    private Vector3 originalPosition;

    private PlayerInputActions inputActions;

    // Stabbing settings
    public float stabDistance = 4f;  // How far the knife will move forward
    public float stabSpeed = 2f;     // How fast the knife will move

    // Reference to the EquipGunOnClick script
    public EquipGunOnClick gunScript;
    public EquipSwordOnClick equipSwordScript;

    // variables for audio
    public AudioClip stabAudioClip; // The sound effect for the swing
    public AudioSource audioSource; // AudioSource component

    void Start()
    {
        knifePrefab = GameManager.Instance.knifePrefab;
        if (equipKnifeButton != null)
        {
            equipKnifeButton.onClick.AddListener(ToggleKnife);
        }
        else
        {
            Debug.LogError("EquipKnifeButton is not assigned in the inspector.");
        }
        // Initialize AudioSource
        audioSource = weaponParent.gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = weaponParent.gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false; // Ensure the sound doesn't play on start
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Stab.performed += ctx => StartStabbing();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Stab.performed -= ctx => StartStabbing();
    }

    void ToggleKnife()
    {
        if (!isEquipped)
        {
            EquipKnife();
        }
        else
        {
            UnequipKnife();
        }
    }

    void EquipKnife()
    {
        // Check if the gun is equipped and unequip it
        if (gunScript != null && gunScript.IsGunEquipped)
        {
            gunScript.UnequipGun();
        }

        // If the sword is equipped, unequip it
        if (equipSwordScript != null && equipSwordScript.IsSwordEquipped)
        {
            equipSwordScript.UnequipSword();
        }

        

        // Instantiate and attach the knife to the WeaponParent
        equippedKnife = Instantiate(knifePrefab, weaponParent);
        equippedKnife.SetActive(true);
        equippedKnife.transform.localPosition = Vector3.zero;
        equippedKnife.transform.localRotation = Quaternion.identity;

        // Set Rigidbody to kinematic
        Rigidbody knifeRigidbody = equippedKnife.GetComponent<Rigidbody>();
        if (knifeRigidbody != null)
        {
            knifeRigidbody.isKinematic = true;
        }

        // Disable any MeshCollider if necessary
        MeshCollider knifeCollider = equippedKnife.GetComponent<MeshCollider>();
        if (knifeCollider != null)
        {
            knifeCollider.enabled = false;
        }

        // Destroy the PlayerNearText script from the knife clone
        PlayerNearText playerNearText = equippedKnife.GetComponent<PlayerNearText>();
        if (playerNearText != null)
        {
            Destroy(playerNearText);
            Debug.Log("Removed PlayerNearText script from the equipped knife.");
        }

        // Destroy the EquipObject script from the knife clone
        EquipObject equipObject = equippedKnife.GetComponent<EquipObject>();
        if (equipObject != null)
        {
            Destroy(equipObject);
            Debug.Log("Removed EquipObject script from the equipped knife.");
        }

        originalPosition = equippedKnife.transform.localPosition;
        isEquipped = true;

        Debug.Log("Knife equipped.");
    }


    public void UnequipKnife()
    {
        if (equippedKnife != null)
        {
            Destroy(equippedKnife);
            equippedKnife = null;
            isEquipped = false;
            Debug.Log("Knife unequipped.");
        }
        
    }

    public bool IsKnifeEquipped => isEquipped; // Public property to check if knife is equipped

    void StartStabbing()
    {
        if (isEquipped && !isStabbing)
        {
            // Play stab audio
            if (stabAudioClip != null && audioSource != null)
            {
                audioSource.clip = stabAudioClip;
                audioSource.Play();
            }
            StartCoroutine(Stab());
        }
    }

    private IEnumerator Stab()
    {
        if (equippedKnife == null)
        {
            Debug.LogError("No knife equipped! Cannot perform stab.");
            yield break;
        }

        isStabbing = true;

        // Move knife forward
        Vector3 targetPosition = originalPosition + equippedKnife.transform.forward * stabDistance;
        Vector3 arcPosition = targetPosition + equippedKnife.transform.up * (stabDistance / 4);
        while (equippedKnife != null && Vector3.Distance(equippedKnife.transform.localPosition, arcPosition) > 0.01f)
        {
            equippedKnife.transform.localPosition = Vector3.MoveTowards(equippedKnife.transform.localPosition, arcPosition, stabSpeed * Time.deltaTime);
            DetectAlienHit();
            yield return null;
        }
        if (equippedKnife != null)
        {
            equippedKnife.transform.localPosition = targetPosition;
            DetectAlienHit();
        }
        yield return new WaitForSeconds(0.1f);

        // Retract knife back to original position
        while (equippedKnife != null && Vector3.Distance(equippedKnife.transform.localPosition, originalPosition) > 0.01f)
        {
            equippedKnife.transform.localPosition = Vector3.MoveTowards(equippedKnife.transform.localPosition, originalPosition, stabSpeed * Time.deltaTime * 2);
            yield return null;
        }

        isStabbing = false;
    }

    private void DetectAlienHit()
    {
        if (equippedKnife == null) return;

        Collider[] hits = Physics.OverlapSphere(equippedKnife.transform.position, 7f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Alien"))
            {
                GreenAlienBehavior alien = hit.GetComponent<GreenAlienBehavior>();
                if (alien != null)
                {
                    alien.TakeDamage();
                    break;
                }

                GAlienS2 alien2 = hit.GetComponent<GAlienS2>();
                if (alien2 != null)
                {
                    alien2.TakeDamage();
                }

                GAlienS3 alien3 = hit.GetComponent<GAlienS3>();
                if (alien3 != null)
                {
                    alien3.TakeDamage();
                }
            }

            if (hit.CompareTag("RhinoAlien"))
            {
                RhinoAlienBehaviour alien = hit.GetComponent<RhinoAlienBehaviour>();
                if (alien != null)
                {
                    alien.TakeDamage();
                    break;
                }
            }
        }
    }
}
