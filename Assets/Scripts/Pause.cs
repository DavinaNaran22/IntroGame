using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    public GameObject PauseScreen; 
    // detect whether the esc key is pressed if so execute the pause_game function
   
    // pauses the game 
   public void Pause_game()
    {
        PauseScreen.SetActive(true);
        // opens the pause screen that allows the user to choose options
        Time.timeScale = 0;
        //pauses the game
   }

    public void Continue_game()
    {
        // hides the pause screen
        PauseScreen.SetActive(false);
        Time.timeScale = 1;
        // starts the game again
    }
}
