using UnityEngine;
using UnityEngine.UI;

public class RhinoDamageBar : MonoBehaviour
{
    public Image damageBarImage; // UI Image representing health bar
    public const float maxAmount = 1f; // Max health (100%)
    public float currentAmount; // Current health

    public delegate void AlienDiedHandler();
    public event AlienDiedHandler OnAlienDied; // Event for alien death

    public delegate void CriticalHealthHandler();
    public event CriticalHealthHandler OnCriticalHealth;

    void Start()
    {
        currentAmount = maxAmount; // Initialize health to full
        UpdateHealthBar(); // Update health bar at the start
    }

    // Call this method to reduce alien's health by a percentage
    public void TakeDamage(float damagePercent)
    {
        currentAmount -= damagePercent;

        Debug.Log("Alien health after taking damage: " + currentAmount * 100 + "%");
        UpdateHealthBar(); // Update the health bar

        if (currentAmount <= 0)
        {
            AlienDeath(); // Deactivate alien when health reaches 0
        }
        else if (currentAmount <= 0.2f) // 20% health threshold
        {
            OnCriticalHealth?.Invoke(); // Notify listeners that the alien is at critical health
        }
    }

    // Update the health bar UI
    void UpdateHealthBar()
    {
        damageBarImage.fillAmount = currentAmount; // Set the fill amount based on current health
    }

    // Handle alien's death (deactivation)
    void AlienDeath()
    {
        Debug.Log("Alien has been deactivated.");
        OnAlienDied?.Invoke(); // Notify listeners that the alien has died
    }
}
