using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeSlider : Singleton<VolumeSlider>
{
    [SerializeField] Slider audioSlider;
    public AudioMixer AudioMixer;

    private void Start()
    {
        audioSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    private void Update()
    {
        // Slider loses ref when going from main -> scene -> main
        if (audioSlider == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main"))
        {
            GameObject sliderGO = GameObject.FindWithTag("VolumeSlider");
            if (sliderGO != null) {
                audioSlider = sliderGO.GetComponent<Slider>();
                Debug.Log("AS Vol " + audioSlider.value + GameManager.Instance.Volume);
                // Ensure slider UI displays same value as GameManager's Volume
                audioSlider.value = GameManager.Instance.Volume;
                audioSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
            }
        }
    }

    private void OnValueChanged()
    {
        AudioMixer.SetFloat("volume", audioSlider.value);
        if (GameManager.Instance) GameManager.Instance.Volume = audioSlider.value;
    }
}
