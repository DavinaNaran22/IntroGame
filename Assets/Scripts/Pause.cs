using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    public GameObject PauseScreen;
    public bool Pause_active = false;
    private GameObject playerCanvas; // Contains the hud text + healthbar

    private void Start()
    {
        playerCanvas = GameObject.Find("PlayerCanvas");
        playerCanvas.SetActive(false);
        Debug.Log(playerCanvas.activeSelf);
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
        Time.timeScale = 1;
        // starts the game again
        // show the player canvas
        playerCanvas.SetActive(true);
    }
}
