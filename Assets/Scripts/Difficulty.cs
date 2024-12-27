public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}

public class Difficulty
{
    // Want to be able to access these values in PlayerHealth but only change them here
    public float initialOxygen { get; private set; }
    public float alienDamage { get; private set; }
    public float herbIncrease { get; private set; }
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
                alienDamage = 0.2f;
                break;
            case DifficultyLevel.Medium:
                alienDamage = 0.3f;
                herbIncrease = 0.1f;
                break;
            case DifficultyLevel.Hard:
                alienDamage = 0.4f;
                herbIncrease = 0.05f;
                break;
        }
    }
}
