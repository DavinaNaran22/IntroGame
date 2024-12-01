using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool unlockedDoor;
    public static GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
}