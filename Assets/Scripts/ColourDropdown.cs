using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class ColourDropdown : Singleton<ColourDropdown>
{
    public ColourMode mode = ColourMode.NoColourBlindness;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Image HealthBar;
    [SerializeField] Color red = new Color(242f, 0f, 0f, 1f);
    [SerializeField] Color blue = new Color(0f, 46f, 255f, 1f);

    void Start()
    {
        
        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });
    }

    void NoColourBlindness()
    {
        HealthBar.color = red;
        Debug.Log("No Colour blindness");
    }

    void Protanopia()
    {
        HealthBar.color = blue;
        Debug.Log("Protanopia");
    }
    void Deuteranopia()
    {
        HealthBar.color = blue;
        Debug.Log("Deuteranopia");
    }

    void Tritanopia()
    {
        HealthBar.color = red;
        Debug.Log("Tritanopia");
    }

    // Use dropdown value to select the correct colour mode
    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                mode = ColourMode.NoColourBlindness;
                NoColourBlindness();
                break;
            case 1:
                mode = ColourMode.Protanopia;
                Protanopia();
                break;
            case 2:
                mode = ColourMode.Deuteranopia;
                Deuteranopia();
                break;
            case 3:
                mode = ColourMode.Tritanopia;
                Tritanopia();
                break;
        }
    }
}
