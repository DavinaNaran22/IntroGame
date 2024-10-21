using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas startScreenCanvas;
    public Canvas hudCanvas;
    public Button playButton;  // Reference to the Play button

    void Start()
    {
        // Initially, show the start screen and hide the HUD
        startScreenCanvas.gameObject.SetActive(true);
        hudCanvas.gameObject.SetActive(false);

        // Add a listener to the button to call StartGame when clicked
        playButton.onClick.AddListener(StartGame);
    }

    // Function to handle starting the game
    public void StartGame()
    {
        // Hide the start screen, show the HUD
        startScreenCanvas.gameObject.SetActive(false);
        hudCanvas.gameObject.SetActive(true);
    }
}