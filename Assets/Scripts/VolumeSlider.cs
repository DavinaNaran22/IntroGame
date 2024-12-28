using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : Singleton<VolumeSlider>
{
    [SerializeField] Slider audioSlider;
    [SerializeField] AudioMixer AudioMixer;

    private void Start()
    {
        audioSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    private void OnValueChanged()
    {
        AudioMixer.SetFloat("volume", audioSlider.value);
    }
}
