using TMPro;
using UnityEngine;

public class ColourDropdown : Singleton<ColourDropdown>
{
    public ColourMode mode = ColourMode.NoColorBlindness;
    [SerializeField] TMP_Dropdown dropdown;

    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });
    }

    // Use dropdown value to select the correct colour mode
    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                mode = ColourMode.NoColorBlindness;
                break;
            case 1:
                mode = ColourMode.Protanopia;
                break;
            case 2:
                mode = ColourMode.Deuteranopia;
                break;
            case 3:
                mode = ColourMode.Tritanopia;
                break;
        }
    }
}
