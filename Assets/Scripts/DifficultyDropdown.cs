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
                difficulty.SetDifficulty(DifficultyLevel.Easy);
                break;
            case 1:
                difficulty.SetDifficulty(DifficultyLevel.Medium);
                break;
            case 2:
                difficulty.SetDifficulty(DifficultyLevel.Hard);
                break;
        }
    }

    void Start()
    {
        difficulty = new Difficulty(DifficultyLevel.Medium);
        dropdown.value = 1;
        AddDropdownDelegate();
    }

    void Update()
    {
        if (GameManager.Instance)
        {
            UpdateDropdownRefs(GameManager.Instance.difficultyDropdown, GameManager.Instance.Difficulty.level);
        }
    }
}
