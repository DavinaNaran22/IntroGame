using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public Image healthBarImage; //image assigned for the foregound/front part of the bar
    public const float maxHealth = 1f; // Health is represented as a percentage (1 is full health)
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Health Bar is at " + currentHealth * 100 + "%");
        UpdateHealthBar();

        StartCoroutine(RegenerateHealth()); // a function to execute at different frames. (restoring health)
    }

    // method to reduce health by a percentage (e.g., 0.05 = 5%)
    public void TakeDamage(float damagePercent)
    {
        currentHealth -= damagePercent;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f; // Ensure health doesn't go negative
            Debug.Log("You Died!");
            RestartScene();
        }
        else
        {
            Debug.Log("Player Health after damage: " + currentHealth * 100 + "%");
            UpdateHealthBar();
        }
    }

    // restores health to 15%
    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            if (currentHealth <= 0.05f && currentHealth > 0f) // If health is at or below 5% and player is alive
            {
                Debug.Log("Critical health! Waiting 8 seconds before regenerating...");
                yield return new WaitForSeconds(8f); // Wait for 8 seconds

                // Check again if the player is still alive (health not 0)
                if (currentHealth > 0f && currentHealth <= 0.05f)
                {
                    currentHealth = 0.15f; // Restore health to 15%
                    Debug.Log("Health restored to 15%!");
                    UpdateHealthBar(); // Update the UI
                }
            }

            yield return null; // Wait for the next frame before checking conditions again
        }
    }


    void UpdateHealthBar()
    {
        healthBarImage.fillAmount = currentHealth;

        // Change color of health bar based on health
        if (currentHealth <= 0.05f)
        {
            healthBarImage.color = Color.red; // Critical health
        }
        else
        {
            healthBarImage.color = Color.green; // Normal health
        }
    }


    // called when player dies and returns to tile A.
    public void RestartScene()
    {
        // Get the active scene's name
        string landscape = SceneManager.GetActiveScene().name;

        // Reload the current scene
        SceneManager.LoadScene("landscape");
    }

}
