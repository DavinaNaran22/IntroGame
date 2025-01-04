using System;
using Unity.VisualScripting;
using UnityEngine;

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}

[Serializable]
public class Difficulty
{
    // Want to be able to access these values in PlayerHealth but only change them here
    public float alienDamage { get; private set; }
    public float medicineIncrease { get; private set; }
    public DifficultyLevel level { get; private set; }

    public Difficulty(DifficultyLevel level)
    {
        this.level = level;
        SetDifficulty(level);
    }

    public void SetDifficulty(DifficultyLevel level)
    {
        this.level = level;
        switch (level)
        {
            case DifficultyLevel.Easy:
                alienDamage = 0.02f;
                medicineIncrease = 0.15f;
                break;
            case DifficultyLevel.Medium:
                alienDamage = 0.04f;
                medicineIncrease = 0.1f;
                break;
            case DifficultyLevel.Hard:
                alienDamage = 0.05f;
                medicineIncrease = 0.05f;
                break;
            default:
                alienDamage = 0.04f;
                medicineIncrease = 0.1f;
                break;
        }
    }

    // For some reason accessing oxygenTime sometimes returns 0
    // After cutscene finished playing in interior
    // This works instead
    // Hours oxygen should last for
    public float GetOxygenTime()
    {
        Debug.Log("In get oxygen time " + level);
        switch (level)
        {
            case DifficultyLevel.Easy:
                return 1f;
            case DifficultyLevel.Medium:
                return 0.75f;
            case DifficultyLevel.Hard:
                return 0.5f;
            default:
                return 0.75f;
        }
    }
}