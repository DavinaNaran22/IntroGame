using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class UISlider : MonoBehaviour, ISlider
{
    public Slider slider;
    public abstract void AddDelegate(Slider slider);


    public abstract void OnValueChanged();

    protected void UpdateRefs(Slider slider, float gmRef, string tagToFind)
    {
        // Slider loses ref when going from main -> scene -> main
        if (slider == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main"))
        {
            GameObject sliderGO = GameObject.FindWithTag(tagToFind);
            if (sliderGO != null)
            {
                Debug.Log(sliderGO.GetComponent<Slider>());
                slider = sliderGO.GetComponent<Slider>();
                AddDelegate(slider);
                // Ensure slider UI displays same value as GameManager's value
                slider.value = gmRef;
            }
        }
    }
}