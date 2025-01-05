using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{    
    public TextMeshProUGUI taskText; // Task description text
    public TextMeshProUGUI progressText; // Progress percentage text
    public Image progressBar; // Image with fillAmount

    private float currentProgress = 0f; // Current progress as a fraction (0 to 1)
    private void Awake()
    {
        // Set in awake so text updates correctly if 1st task completed
        // And player hasn't opened progress before completing it
        taskText.text = "Task: Find control panel";
    }

    void Start()
    {
        // If player loads and they've made progress we don't want to reset it
        if (GameManager.Instance.currentProgress != 0) return;
        progressBar.fillAmount = 0f;
        UpdateProgressUI();
        SetGameManagerVars();
    }

    private void SetGameManagerVars()
    {
        GameManager.Instance.currentTask = taskText.text;
        GameManager.Instance.progressText = progressText.text;
        GameManager.Instance.currentProgress = currentProgress;
    }

    public void SetTaskText(string taskDescription)
    {
        taskText.text = taskDescription;
        Debug.Log("New Task Text " + taskText.text);
        GameManager.Instance.currentTask = taskDescription;
    }

    public void IncreaseProgress(float percentage)
    {
        Debug.Log("Progress added by " + percentage);
        float progressIncrement = percentage / 100f;
        currentProgress = Mathf.Clamp(currentProgress + progressIncrement, 0f, 1f); // Ensure progress stays between 0 and 1
        UpdateProgressUI();
    }

    private void UpdateProgressUI()
    {
        string updatedText = "Progress: " + Mathf.RoundToInt(currentProgress * 100) + "%";
        progressBar.fillAmount = currentProgress;
        GameManager.Instance.currentProgress = currentProgress;
        progressText.text = updatedText;
        GameManager.Instance.progressText = updatedText;
    }

    public float GetProgress()
    {
        return currentProgress;
    }

    public void LoadProgress()
    {
        progressBar.fillAmount = GameManager.Instance.currentProgress;
        progressText.text = GameManager.Instance.progressText;
        taskText.text = GameManager.Instance.currentTask;
    }
}
