using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class OxygenPercent : MonoBehaviour
{
    public Image OxygenBarImage;
    public TextMeshProUGUI oxygenText;
    private bool initialDelayCompleted = false;

    void Start()
    {
        // Start the delay coroutine
        StartCoroutine(InitialDelay());
    }

    IEnumerator InitialDelay()
    {
        // Set the percentage to 100% for the initial 5 seconds
        oxygenText.text = "Oxygen: 100%";

        // Wait for 5 seconds
        yield return new WaitForSeconds(14);

        // Set flag to true after delay
        initialDelayCompleted = true;
    }

    void Update()
    {
        // Only update the percentage if the initial delay is completed
        if (initialDelayCompleted)
        {
            UpdatePercentageText();
        }
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
