using UnityEngine;

public class PlayerNearDoor : PlayerNearText
{
    new void Update()
    {
        base.Update();
        if (GameManager.Instance.unlockedDoor)
        {
            // Prevents hover text from briefly flickering to 'Right click to open keypad'
            // Upon the first time player right clicks door
            Text = "Right click to leave ship";
        }
    }
}
