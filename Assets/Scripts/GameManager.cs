using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool unlockedDoor;
    public GameObject player;
    public TextMeshProUGUI hoverText;
    public bool playFirstCutscene;
}