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
        progressBar.fillAmount = 0f;
        UpdateProgressUI();
    }

    public void SetTaskText(string taskDescription)
    {
        taskText.text = taskDescription;
        Debug.Log("New Task Text " + taskText.text);
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
        progressBar.fillAmount = currentProgress;
        progressText.text = "Progress: " + Mathf.RoundToInt(currentProgress * 100) + "%";
    }

    public float GetProgress()
    {
        return currentProgress;
    }
}
