using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OxygenDisplay : MonoBehaviour
{
    public TextMeshProUGUI oxygenText; // Link this to a TextMeshPro UI element
    public Button startButton; // Link this to your Button UI element
    public float totalOxygenTime = 7200f; // 2 hours in seconds
    private float currentOxygenTime;
    private bool countdownStarted = false; // To track whether the countdown has started

    void Start()
    {
        currentOxygenTime = 3f; // Initialize full oxygen time
        UpdateOxygenText(); // Initialize the display

        // Ensure the countdown starts only when the button is pressed
        startButton.onClick.AddListener(StartCountdown);
    }

    void Update()
    {
        if (countdownStarted && currentOxygenTime > 0)
        {
            currentOxygenTime -= Time.deltaTime; // Decrease oxygen time over real time
            UpdateOxygenText();
        }
        else if (currentOxygenTime <= 0)
        {
            currentOxygenTime = 0; // Ensure the countdown stops at 0
            Debug.Log("Oxygen is run out :(");
            UpdateOxygenText();
            Application.Quit();
        }
    }

    void StartCountdown()
    {
        countdownStarted = true; // Start the countdown
    }

    void UpdateOxygenText()
    {
        // Calculate time left
        int hours = Mathf.FloorToInt(currentOxygenTime / 3600); // Get hours
        int minutes = Mathf.FloorToInt((currentOxygenTime % 3600) / 60); // Get minutes
        int seconds = Mathf.FloorToInt(currentOxygenTime % 60); // Get seconds

        // Calculate percentage left
        float percentage = (currentOxygenTime / totalOxygenTime) * 100f;

        // Format time to always show two digits
        string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        // Update TextMeshPro UI
        oxygenText.text = string.Format("Oxygen Levels: {0:0}%\nTime Left: {1}", percentage, timeString);
    }
}
