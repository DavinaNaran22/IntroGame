using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxygenDisplay : MonoBehaviour
{
    public Image OxygenBarImage; // Image for the oxygen bar
    public PlayerHealth health; // Object of player's health to access restart function
    public float maxOxygen; // Total oxygen time (2 hours in seconds)
    private float currentOxygenTime;

    void Start()
    {
        maxOxygen = HoursToSeconds(1); // Initialize with full oxygen time (1 hour)
        currentOxygenTime = HoursToSeconds(GameManager.Instance.Difficulty.GetOxygenTime()); // Set correct oxygen time
        if (SceneManager.GetActiveScene().name == "landscape")
        {
            UpdateOxygenBar();
        }
    }

    void Update()
    {
        if (currentOxygenTime > 0)
        {
            currentOxygenTime -= Time.deltaTime; // Decrease oxygen time over real-time
            UpdateOxygenBar();
        }
        else if (currentOxygenTime <= 0)
        {
            currentOxygenTime = 0; // Ensure the countdown stops at 0
            Debug.Log("Oxygen has run out :(");
            health.Die(); // Restart scene when oxygen runs out
        }
    }

    void UpdateOxygenBar()
    {
        // Update the fill amount of the oxygen bar based on current oxygen time
        OxygenBarImage.fillAmount = currentOxygenTime / maxOxygen;
    }

    // Used for maxOxygen value
    float HoursToSeconds(float hours)
    {
        return hours * 60 * 60;
    }
}
