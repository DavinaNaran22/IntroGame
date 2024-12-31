using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedSlider : UISlider
{
    public float GameTime;
    private float fixedDeltaTime;

    private void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }
    void Start()
    {
        GameTime = 1;
        AddDelegate(slider);
    }

    void Update()
    {
        if (GameManager.Instance) UpdateRefs(slider, GameManager.Instance.GameTime, "GameTimeSlider");
    }

    public override void AddDelegate(Slider slider)
    {
        this.slider = slider;
        this.slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public override void OnValueChanged()
    {
        GameTime = slider.value;
        Time.timeScale = slider.value;
        // Need to adjust fixed delta time as well
        Time.fixedDeltaTime =  fixedDeltaTime * Time.timeScale;
        if (GameManager.Instance) GameManager.Instance.GameTime = slider.value;
    }
}
