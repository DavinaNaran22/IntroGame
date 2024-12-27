public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}

public class Difficulty
{
    // Want to be able to access these values in PlayerHealth but only change them here
    public float oxygenTime { get; private set; } // Time oxygen lasts in hours
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
        switch (level)
        {
            case DifficultyLevel.Easy:
                oxygenTime = 2;
                alienDamage = 0.2f;
                medicineIncrease = 0.15f;
                break;
            case DifficultyLevel.Medium:
                oxygenTime = 1.5f;
                alienDamage = 0.3f;
                medicineIncrease = 0.1f;
                break;
            case DifficultyLevel.Hard:
                oxygenTime = 1f;
                alienDamage = 0.4f;
                medicineIncrease = 0.05f;
                break;
        }
    }
}
