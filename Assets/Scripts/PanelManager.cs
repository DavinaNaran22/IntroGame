using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public List<string> tasks; // List of task descriptions
    public TextMeshProUGUI taskText; // Task description text
    public TextMeshProUGUI progressText; // Progress percentage text
    public Image progressBar; // Image with fillAmount
    public List<GameObject> aliens;

    private int currentTaskIndex = 0;

    void Start()
    {
        progressBar.fillAmount = 0f;
        UpdateTaskUI();
    }

    

    // Function to check conditions for each task based on its index
    private bool IsTaskConditionMet()
    {
        switch (currentTaskIndex)
        {
            case 0:
                // Conditions for Task 0
                //Example to test when alien1 is defeated.
                if (!aliens[0].activeSelf)
                {
                    return true;
                }
                break;

            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    return true;
                }

                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    return true;
                }
                break;

                // Add more cases as needed for additional tasks
                // Each case should return true if the specific conditions for that task are met
        }

        // If none of the conditions are met, return false
        return false;
    }

    private void CompleteTask()
    {
        if (currentTaskIndex < tasks.Count)
        {
            currentTaskIndex++;
            UpdateProgress();
            UpdateTaskUI();
        }
    }

    void Update()
    {
        if (IsTaskConditionMet())
        {
            CompleteTask();
        }
        {
            
        }
    }

        private void UpdateTaskUI()
    {
        if (currentTaskIndex < tasks.Count)
        {
            taskText.text = "Task: " + tasks[currentTaskIndex];
        }
        else
        {
            taskText.text = "All tasks completed!";
        }
    }

    private void UpdateProgress()
    {
        float progress = (float)currentTaskIndex / tasks.Count;
        progressBar.fillAmount = progress;
        progressText.text = "Progress: " + Mathf.RoundToInt(progress * 100) + "%";
    }
}
