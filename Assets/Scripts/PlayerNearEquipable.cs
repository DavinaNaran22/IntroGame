using UnityEngine;

public class PlayerNearEquipable : PlayerNearText
{
    [SerializeField] GameObject equipableObject;
    private PlayerInputActions inputActions;
    public static int count = 0;

    private EquipManager equipManager;
    private EquipData objectData;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }
   
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Equip.performed += ctx => EquipObject();
    }

    private void Start()
    {
        equipManager = GameManager.Instance.equipManager;
        objectData = equipManager.GetFromEquipList(equipableObject);
        if (objectData != null)
        {
            Debug.Log("object check equip");
            // Deactivate if already been equipped
            if (objectData.hasBeenEquiped)
            {
                equipableObject.SetActive(false);
            }
            Debug.Log("object not been equipped");
        }
        else
        {
            objectData = new EquipData(equipableObject, false);
            equipManager.equipObjects.Add(objectData);
        }
    }

    new void OnDisable()
    {
        base.OnDisable();
        inputActions.Player.Disable();
        inputActions.Player.Equip.performed -= ctx => EquipObject();
    }

    // Only equip if hover text is showing and this is the one showing the text
    // Hover text only shows if player is near
    void EquipObject()
    {
        if (hoverText.text == Text && modifyingText)
        {
            objectData.hasBeenEquiped = true;
            equipableObject.SetActive(false);
            count += 1;
            Debug.Log(count);
        }
    }
}
