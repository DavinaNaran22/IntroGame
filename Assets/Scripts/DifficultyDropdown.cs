using TMPro;
using UnityEngine;

public class DifficultyDropdown : Dropdown
{
    public Difficulty difficulty;
    public override void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                difficulty.SetDifficulty(DifficultyLevel.Medium);
                break;
            case 1:
                difficulty.SetDifficulty(DifficultyLevel.Easy);
                break;
            case 2:
                difficulty.SetDifficulty(DifficultyLevel.Hard);
                break;
            default:
                difficulty.SetDifficulty(DifficultyLevel.Medium);
                break;
        }

        if (GameManager.Instance)
        { 
            GameManager.Instance.Difficulty = difficulty;
            Debug.Log("New game manager difficulty " + GameManager.Instance.Difficulty.level);
        }
    }

    void Start()
    {
        difficulty = new Difficulty(DifficultyLevel.Medium);
        AddDropdownDelegate();
    }

    void Update()
    {
        if (GameManager.Instance)
        {
            if (GameManager.Instance.Difficulty == null) GameManager.Instance.Difficulty = difficulty;
            UpdateDropdownRefs(GameManager.Instance.difficultyDropdown, GameManager.Instance.Difficulty.level);
        }
    }
}
