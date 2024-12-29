using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : UISlider
{
    public AudioMixer AudioMixer;

    public override void AddDelegate(Slider slider)
    {
        this.slider = slider;
        this.slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    private void Start()
    {
        AddDelegate(slider);
    }

    private void Update()
    {
        if (GameManager.Instance) UpdateRefs(slider, GameManager.Instance.Volume, "VolumeSlider");
    }

    public override void OnValueChanged()
    {
        AudioMixer.SetFloat("volume", slider.value);
        if (GameManager.Instance) GameManager.Instance.Volume = slider.value;
    }
}