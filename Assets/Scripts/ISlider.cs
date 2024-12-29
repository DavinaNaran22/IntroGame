using UnityEngine;
using UnityEngine.UI;
interface ISlider
{
    void AddDelegate(Slider slider);
    void OnValueChanged();

}
