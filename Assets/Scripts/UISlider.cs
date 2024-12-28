using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class UISlider : ISlider
{
    public Slider slider;
    public abstract void AddDelegate();


    public abstract void OnValueChanged();

    protected void UpdateRefs(Slider slider, float gmRef, string tagToFind)
    {
        // Slider loses ref when going from main -> scene -> main
        if (slider == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main"))
        {
            GameObject sliderGO = GameObject.FindWithTag(tagToFind);
            if (sliderGO != null)
            {
                slider = sliderGO.GetComponent<Slider>();
                // Ensure slider UI displays same value as GameManager's value
                slider.value = gmRef;
                AddDelegate();
            }
        }
    }
}
