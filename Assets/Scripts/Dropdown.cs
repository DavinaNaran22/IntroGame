using System;
using TMPro;
using UnityEngine;

public abstract class Dropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public abstract void DropdownValueChanged(TMP_Dropdown dropdown);

    public void AddDropdownDelegate()
    {
        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });
    }

    // Keeping comments from original ColourDropdown to explain
    public void UpdateDropdownRefs<T>(TMP_Dropdown gameManagerDropdown, T gmDropdownEnum) where T : Enum
    {
        // After player presses play, ref to original colour dropdown is gone
        // Have to update to reference new one
        // And the new one needs to have the event listener added to it for mode switch to work
        // TODO This can be removed if it's possible for GameManager to be in Main scene
        if (dropdown == null && gameManagerDropdown != null)
        {
            dropdown = gameManagerDropdown;
            AddDropdownDelegate();
        }

        // Selected dropdown value will default to Normal whenever it switches to main menu scene
        // Need the selected value to match the currently selected option
        // Check that everything's been assigned and that there's a mismatch between the values
        if (GameManager.Instance != null && dropdown != null && dropdown.value != (int)(object)gmDropdownEnum) // cast to object to allow cast to int
        {
            dropdown.value = (int)(object)gmDropdownEnum;
        }
    }
}
