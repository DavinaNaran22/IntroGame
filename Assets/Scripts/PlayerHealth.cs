using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;


public class PlayerHealth : MonoBehaviour
{
    public Image healthBarImage; //image assigned for the foregound/front part of the bar
    public const float maxHealth = 1f; // Health is represented as a percentage (1 is full health)
    public float currentHealth;
    private bool isRegenerating = false; // Flag to ensure only one coroutine runs at a time
    private bool isEasyRegenerating = false;
    private bool hasDied = false; // Just in case update and take damage both call Die()


    void Start()
    {
        GameManager.Instance.colourScript.AddToImageList(ColourChangeColours.Green, healthBarImage);
        currentHealth = maxHealth;
        GameManager.Instance.playerHealth = currentHealth;
        Debug.Log("Health Bar is at " + currentHealth * 100 + "%");
        UpdateHealthBar();
    }

    private void Update()
    {
        if (GameManager.Instance.Difficulty.level == DifficultyLevel.Easy && currentHealth > 0f && currentHealth < 1f && !isEasyRegenerating)
        {
            StartCoroutine(EasyHealthRegen());
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Regeneration for Easy difficulty - regen every 10s
    IEnumerator EasyHealthRegen()
    {
        // Only want one EasyHealthRegen
        isEasyRegenerating = true;
        yield return new WaitForSeconds(10f);

        while (currentHealth > 0f && currentHealth < 1f)
        {
            Debug.Log("Regenerating health on easy mode");
            Heal(); // Will heal player by 15% (easy mode medicine increase) and update UI
            yield return new WaitForSeconds(10f);
        }

        if (currentHealth == 1f)
        {
            isEasyRegenerating = false;
            yield break;
        }
    }

    // method to reduce health by a percentage (e.g., 0.05 = 5%)
    public void TakeDamage(float damagePercent)
    {
        currentHealth -= damagePercent;
        GameManager.Instance.playerHealth = currentHealth;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f; // Ensure health doesn't go negative
            Debug.Log("You Died!");
            Die();
        }
        else
        {
            Debug.Log("Player Health after damage: " + currentHealth * 100 + "%");
            UpdateHealthBar();

            // Start regenerating health if it's in the critical range and not already regenerating
            if (currentHealth <= 0.05f && !isRegenerating)
            {
                StartCoroutine(RegenerateHealth());
            }
        }
    }

    // restores health to 15%
    IEnumerator RegenerateHealth()
    {
        isRegenerating = true; // Mark regeneration as active

        if (currentHealth <= 0.05f && currentHealth > 0f) // If health is at or below 5% and player is alive
        {
            Debug.Log("Critical health! Waiting 8 seconds before regenerating...");
            yield return new WaitForSeconds(8f); // Wait for 8 seconds

            // Check again if the player is still alive (health not 0)
            if (currentHealth > 0f && currentHealth <= 0.05f)
            {
                currentHealth = 0.15f; // Restore health to 15%
                GameManager.Instance.playerHealth = currentHealth;
                Debug.Log("Health restored to 15%!");
                UpdateHealthBar(); // Update the UI
            }
        }

        isRegenerating = false; // Allow future regeneration if health goes critical again
    }

    void UpdateHealthBar()
    {
        healthBarImage.fillAmount = currentHealth;

        // Temp change for colourblind - prot/deut can't see red/green
        if (GameManager.Instance.colourMode == ColourMode.NoColourBlindness)
        {
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
    }

    // called when player dies and returns to tile A.
    public void Die()
    {
        if (!hasDied)
        {
            hasDied = true;
            StartCoroutine(LoadDeathSceneAsync());
        }
    }

    IEnumerator LoadDeathSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Death");
        // Wait for scene to finish loading async
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void Heal()
    {
        currentHealth = Math.Clamp(currentHealth + GameManager.Instance.Difficulty.medicineIncrease, 0f, maxHealth);
        GameManager.Instance.playerHealth = currentHealth;
        UpdateHealthBar();
    }
}