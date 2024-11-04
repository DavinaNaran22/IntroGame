using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxygenPercent : MonoBehaviour
{
    public Image OxygenBarImage;
    public TextMeshProUGUI oxygenText;

    void Update()
    {
        UpdatePercentageText();
    }

    void UpdatePercentageText()
    {
        // Calculate the percentage based on fillAmount (0 to 1) and convert it to a 0-100 range
        int percentage = (int)(OxygenBarImage.fillAmount * 100f);

        if (OxygenBarImage.fillAmount > 0 && percentage == 0) //checks for percentage to be zero on exact zero
        {
            percentage = 1;
        }
        else if(OxygenBarImage.fillAmount == 0)
        {
            percentage = 0;
        }

        // Update the text to display the percentage with no decimal places
        oxygenText.text = "Oxygen: " + percentage.ToString() + "%";
    }
}
