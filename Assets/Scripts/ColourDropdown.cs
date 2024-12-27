using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColourDropdown : Dropdown
{
    public ColourMode mode;
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

    //public override TMP_Dropdown dropdown { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Start()
    {
        mode = ColourMode.NoColourBlindness;
        NoColourBlindness();
        AddDropdownDelegate();
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
    public override void DropdownValueChanged(TMP_Dropdown dropdown)
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
        if (GameManager.Instance)
        {
            UpdateDropdownRefs(GameManager.Instance.colourDropdown, GameManager.Instance.colourMode);
        }
    }
}
