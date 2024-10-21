using UnityEngine;
using UnityEngine.UI;

public class AlienDamageBar : MonoBehaviour
{
    public Image damageBarImage; // UI Image representing health bar
    public const float maxAmount = 1f; // Max health (100%)
    private float currentAmount; // Current health

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
    }

    // Update the health bar UI
    void UpdateHealthBar()
    {
        damageBarImage.fillAmount = currentAmount; // Set the fill amount based on current health
    }

    // Handle alien's death (deactivation)
    void AlienDeath()
    {
        gameObject.SetActive(false); // Deactivate the alien game object
        Debug.Log("Alien has been deactivated.");
        if (transform.parent != null) // Check if the GameObject has a parent
        {
            transform.parent.gameObject.SetActive(false); // Deactivate the parent
        }
        else
        {
            Debug.Log("This object has no parent.");
        }
        
    }
}
