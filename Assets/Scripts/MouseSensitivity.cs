using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : UISlider
{
    public override void AddDelegate(Slider slider)
    {
        this.slider = slider;
        this.slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public override void OnValueChanged()
    {
        MouseLook.mouseSensitivity = slider.value;
        if (GameManager.Instance) GameManager.Instance.MouseSens = MouseLook.mouseSensitivity;
    }

    void Start()
    {
        AddDelegate(slider);
    }

    void Update()
    {
        if (GameManager.Instance) UpdateRefs(slider, GameManager.Instance.MouseSens, "MouseSlider");
    }
}