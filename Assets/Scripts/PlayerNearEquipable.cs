using UnityEngine;

public class PlayerNearEquipable : PlayerNearText
{
    [SerializeField] GameObject equipableObject;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Equip.performed += ctx => EquipObject();
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
            equipableObject.SetActive(false);
        }
    }
}
