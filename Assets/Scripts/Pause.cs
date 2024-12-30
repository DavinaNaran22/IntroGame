using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    public GameObject PauseScreen;
    public bool Pause_active = false;
    private GameObject playerCanvas; // Contains the hud text + healthbar
    private float fixedDeltaTime;

    private void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Start()
    {
        playerCanvas = GameObject.Find("PlayerCanvas");
        playerCanvas.SetActive(false);
;    }

    void Update()
    {
        if (Pause_active == true)
        {
            Pause_game();
            playerCanvas.SetActive(false);
        }
        else {
            Continue_game();
        }
    }
    // pauses the game 
    public void Pause_game()
    {
        Pause_active = true;
        PauseScreen.SetActive(true);
        // opens the pause screen that allows the user to choose options
        Time.timeScale = 0;
        //pauses the game
   }

    public void Continue_game()
    {
        // hides the pause screen
        Pause_active = false;
        PauseScreen.SetActive(false);
        // starts the game again which user's game time
        Time.timeScale = GameManager.Instance.GameTime;
        Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        // show the player canvas
        playerCanvas.SetActive(true);
    }
}
