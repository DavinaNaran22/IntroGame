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
        Debug.Log("Health Bar is at " + currentHealth * 100 + "%");
        UpdateHealthBar();
    }

    // Call this method to reduce health by a percentage (e.g., 0.05 = 5%)
    public void TakeDamage(float damagePercent)
    {
        currentHealth -= damagePercent;
        if (currentHealth <= 0.00)
        {
            Debug.Log("You are dead!");
            
        }
        Debug.Log("Player Health after damage: " + currentHealth * 100 + "%");
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBarImage.fillAmount = currentHealth;
        
    }
}
