using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBarImage; 
    public const float maxHealth = 1f; // Health is represented as a percentage (1 is full health)
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Health Bar is at " + currentHealth * 100 + "%");
        UpdateHealthBar();

        StartCoroutine(RegenerateHealth());
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

    // Coroutine to regenerate health
    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Wait for 5 seconds

            if (currentHealth < maxHealth)
            {
                currentHealth += 0.02f; // Restore 2% of health
                currentHealth = Mathf.Min(currentHealth, maxHealth); // Ensure it doesn't exceed maxHealth
                Debug.Log("Player Health after regeneration: " + currentHealth * 100 + "%");
                UpdateHealthBar();
            }
        }
    }
    void UpdateHealthBar()
    {
        healthBarImage.fillAmount = currentHealth;
        
    }
}
