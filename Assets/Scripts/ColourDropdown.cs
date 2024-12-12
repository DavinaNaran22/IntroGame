using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class ColourDropdown : Singleton<ColourDropdown>
{
    public ColourMode mode;
    [SerializeField] TMP_Dropdown dropdown;
    // Drag prefabs to these
    [SerializeField] Image HealthBar;
    [SerializeField] Image EnemyMinimapIcon;
    [SerializeField] Image RedSubtitleBox;
    [SerializeField] Image GreenSubtitleBox;
    [SerializeField] TextMeshProUGUI PromptText;
    [SerializeField] Image ExitButton;
    [SerializeField] Image PlayerHealthBar;
    [SerializeField] Color red = new Color(242f, 0f, 0f, 255f);
    [SerializeField] Color blue = new Color(0f, 46f, 255f, 255f);
    [SerializeField] Color green = new Color(48f, 215f, 0f, 1f);
    [SerializeField] Color yellow = new Color(255f, 193f, 7f, 255f); // FFC107

    void Start()
    {
        mode = ColourMode.NoColourBlindness;
        NoColourBlindness();
        print("In dropdown start");
        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });
    }

    // Set colours back to original red
    void SetColourRed()
    {
        HealthBar.color = red;
        EnemyMinimapIcon.color = red;
        RedSubtitleBox.color = red;
        PromptText.color = red;
        ExitButton.color = red;
    }

    // Set colours back to original green
    void SetColourGreen()
    {
        PlayerHealthBar.color = green;
        GreenSubtitleBox.color = green;
    }

    // Make red images blue
    void SetColourBlue()
    {
        HealthBar.color = blue;
        EnemyMinimapIcon.color = blue;
        RedSubtitleBox.color = blue;
        PromptText.color = blue;
        ExitButton.color = blue;
    }

    // Make green images yellow
    void SetColourYellow()
    {
        PlayerHealthBar.color = yellow;
        GreenSubtitleBox.color = yellow;
    }

    void NoColourBlindness()
    {
        SetColourRed();
        SetColourGreen();
        Debug.Log("No Colour blindness");
    }

    void Protanopia()
    {
        SetColourBlue();
        SetColourYellow();
        Debug.Log("Protanopia");
    }
    void Deuteranopia()
    {
        SetColourBlue();
        SetColourYellow();
        Debug.Log("Deuteranopia");
    }

    void Tritanopia()
    {
        SetColourRed();
        SetColourGreen();
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
            default:
                mode = ColourMode.NoColourBlindness;
                NoColourBlindness();
                break;
        }
        
        if (GameManager.Instance) GameManager.Instance.colourMode = mode;
    }

    private void Update()
    {
        
        // After player presses play, ref to original colour dropdown is gone
        // Have to update to reference new one
        // And the new one needs to have the event listener added to it for mode switch to work
        // TODO This can be removed if it's possible for GameManager to be in Main scene
        if (dropdown == null && GameManager.Instance.colourDropdown != null)
        {
            dropdown = GameManager.Instance.colourDropdown;
            dropdown.onValueChanged.AddListener(delegate
            {
                DropdownValueChanged(dropdown);
            });
        }

        // Selected dropdown value will default to Normal whenever it switches to main menu scene
        // Need the selected value to match the currently selected option
        // Check that everything's been assigned and that there's a mismatch between the values
        if (GameManager.Instance != null && dropdown != null && dropdown.value != (int) GameManager.Instance.colourMode)
        {
            dropdown.value = (int)GameManager.Instance.colourMode;
        }
    }
}
