using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxygenDisplay : MonoBehaviour
{
    public TextMeshProUGUI oxygenText; // display text for oxygen
    public PlayerHealth health;  //object of player's health to access restart function
    public const float totalOxygenTime = 7200f; // 2 hours in seconds
    private float currentOxygenTime;
  


    void Start()
    {
        currentOxygenTime = totalOxygenTime; // Initialize full oxygen time
        if(SceneManager.GetActiveScene().name == "landscape")
        {
            UpdateOxygenText();
        }
    }

    void Update()
    {
        if (currentOxygenTime > 0)
        {
            currentOxygenTime -= Time.deltaTime; // Decrease oxygen time over real time
            UpdateOxygenText();
        }
        else if (currentOxygenTime <= 0)
        {
            currentOxygenTime = 0; // Ensure the countdown stops at 0
            Debug.Log("Oxygen is run out :(");
            UpdateOxygenText();
            health.RestartScene();
        }
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
