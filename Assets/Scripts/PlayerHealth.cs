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
        if (currentHealth <= 0.00)
        {
            Debug.Log("You Died!");
            RestartScene();
            
        }
        Debug.Log("Player Health after damage: " + currentHealth * 100 + "%");
        UpdateHealthBar();
    }

    // Coroutine to regenerate health
    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(8f); // Wait for 8 seconds

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

    // called when player dies and returns to tile A.
    public void RestartScene()
    {
        // Get the active scene's name
        string landscape = SceneManager.GetActiveScene().name;

        // Reload the current scene
        SceneManager.LoadScene("landscape");
    }

}
