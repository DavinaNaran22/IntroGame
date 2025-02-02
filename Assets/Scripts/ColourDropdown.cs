using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ColourChangeTypes
{
    Image,
    Text
}

public enum ColourChangeColours
{
    Red,
    Green
}

public class ColourDropdown : Singleton<ColourDropdown>
{
    public ColourMode mode;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] int curDropdownValue;

    [Header("Red")]
    public List<TextMeshProUGUI> RedText = new List<TextMeshProUGUI>();
    public List<Image> RedImages = new List<Image>();
    public List<Light> RedLights = new List<Light>();

    [Header("Green")]
    public List<TextMeshProUGUI> GreenText = new List<TextMeshProUGUI>();
    public List<Image> GreenImages = new List<Image>();

    [SerializeField] Material greenLight;
    [SerializeField] Material redCrystal;

    //[SerializeField] Image RedSubtitleBox;
    //[SerializeField] Image GreenSubtitleBox;
    [SerializeField] Color red = new Color(242f, 0f, 0f, 255f);
    [SerializeField] Color blue = new Color(0f, 46f, 255f, 255f);
    [SerializeField] Color green = new Color(48f, 215f, 0f, 1f);
    [SerializeField] Color yellow = new Color(255f, 193f, 7f, 255f); // FFC107

    void Start()
    {
        mode = ColourMode.NoColourBlindness;
        NoColourBlindness();
        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });
    }

    // Have to check if null since Some items might be null if they lose reference between scenes
    // Better solution would remove the null items...
    // Set colours back to original red
    void SetColourRed()
    {
        foreach (var text in RedText)
        {
            if (text != null) text.color = red;
        }
        foreach (var image in RedImages)
        {
            if (image != null) image.color = red;
        }
        foreach (var light in RedLights)
        {
            if (light != null) light.color = red;
        }
        redCrystal.color = red;
        //RedSubtitleBox.color = red;
    }

    // Set colours back to original green
    void SetColourGreen()
    {
        foreach (var text in GreenText)
        {
            if (text != null) text.color = green;
        }
        foreach (var image in GreenImages)
        {
            if (image != null) image.color = green;
        }
        greenLight.color = green;
        //GreenSubtitleBox.color = green;
    }

    // Make red stuff blue
    void SetRedToBlue()
    {
        foreach (var image in RedImages)
        {
            if (image != null) image.color = blue;
        }
        foreach (var text in RedText)
        {
            if (text != null) text.color = blue; 
        }
        foreach (var light in RedLights)
        {
            if (light != null) light.color = blue;
        }
        redCrystal.color = blue;
        //RedSubtitleBox.color = blue;
    }

    // Make green stuff yellow
    void SetGreenToYellow()
    {
        foreach (var text in GreenText)
        {
            if (text != null) text.color = yellow;
        }
        foreach (var image in GreenImages)
        {
            if (image != null) image.color = yellow;
        }
        greenLight.color = yellow;
        //GreenSubtitleBox.color = yellow;
    }

    // Methods to change colours according to colour mode
    void NoColourBlindness()
    {
        SetColourRed();
        SetColourGreen();
    }

    void Protanopia()
    {
        SetRedToBlue();
        SetGreenToYellow();
    }

    void Deuteranopia()
    {
        SetRedToBlue();
        SetGreenToYellow();
    }

    void Tritanopia()
    {
        SetColourRed();
        SetColourGreen();
    }

    // Add image to correct list and update its colour
    public void AddToImageList(ColourChangeColours colour, Image image)
    {
        if (colour == ColourChangeColours.Red)
        {
            RedImages.Add(image);
        } else if (colour == ColourChangeColours.Green)
        {
           GreenImages.Add(image);
        }
        UpdateColours();
    }

    // Add text to correct list and update its colour
    public void AddToTextList(ColourChangeColours colour, TextMeshProUGUI text)
    {
        if (colour == ColourChangeColours.Red)
        {
            RedText.Add(text);
        }
        else if (colour == ColourChangeColours.Green)
        {
            GreenText.Add(text);
        }

        UpdateColours();
    }

    // Add light to correct list and update its colour
    public void AddToLightList(ColourChangeColours colour, Light light)
    {
        if (colour == ColourChangeColours.Red)
        {
            RedLights.Add(light);
        }
        UpdateColours();
    }

    // Called after image/text added to list so it gets the correct colour
    public void UpdateColours()
    {
        switch (GameManager.Instance.colourMode)
        {
            case ColourMode.NoColourBlindness:
                NoColourBlindness();
                break;
            case ColourMode.Protanopia:
                Protanopia();
                break;
            case ColourMode.Deuteranopia:
                Deuteranopia();
                break;
            case ColourMode.Tritanopia:
                Tritanopia();
                break;
        }
    }

    // Use dropdown value to select the correct colour mode
    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        curDropdownValue = dropdown.value;
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
