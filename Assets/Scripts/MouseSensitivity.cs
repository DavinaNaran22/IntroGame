using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : UISlider, ISlider
{
    [SerializeField] Slider slider;
    public void AddDelegate()
    {
        slider.onValueChanged.AddListener(delegate {  OnValueChanged(); });
    }

    public void OnValueChanged()
    {
        MouseLook.mouseSensitivity = slider.value;
        if (GameManager.Instance) GameManager.Instance.mouseSens = MouseLook.mouseSensitivity;
    }

    void Start()
    {
        AddDelegate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
