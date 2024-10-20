using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBarImage; // Drag the health bar image here in the Inspector
    public const float maxHealth = 1f; // Health is represented as a percentage (1 is full health)
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Call this method to reduce health by a percentage (e.g., 0.05 = 5%)
    public void TakeDamage(float damagePercent)
    {
        currentHealth -= damagePercent;
        if (currentHealth <= 0)
        {
            Debug.Log("You are dead!");
        }
        Debug.Log("Player Health after damage: " + currentHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBarImage.fillAmount = currentHealth;
        // Update the fill amount of the health bar image based on current health
        Debug.Log("Health Bar Fill Amount: " + healthBarImage.fillAmount);
    }
}
