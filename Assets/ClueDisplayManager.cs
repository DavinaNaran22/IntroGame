using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class ClueDisplayManager : MonoBehaviour
{
    public GameObject[] clues; // Array of clue GameObjects
    public Sprite[] clueSprites; // Array of clue sprites
    public GameObject panel; // Panel GameObject
    public Image clueImage; // Image component (child of the panel)
    public TextMeshProUGUI clueCounterText;

    private int cluesFound = 0; // Tracks how many clues have been found

    private void Start()
    {
        panel.SetActive(false); // Ensure the panel is inactive at the start
        UpdateClueCounter(); // Initialize the counter display
    }

    private void Update()
    {
        for (int i = 0; i < clues.Length; i++)
        {
            if (clues[i] != null && !clues[i].activeInHierarchy) // Check if a clue is inactive
            {
                StartCoroutine(DisplayClue(i)); // Display the clue
                clues[i] = null; // Prevent re-checking the same clue
                cluesFound++; // Increment the clues found counter
                UpdateClueCounter(); // Update the counter display
            }
        }
    }

    private IEnumerator DisplayClue(int clueIndex)
    {
        panel.SetActive(true); // Show the panel
        clueImage.sprite = clueSprites[clueIndex]; // Set the sprite on the child Image
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        panel.SetActive(false); // Hide the panel
    }

    private void UpdateClueCounter()
    {
        clueCounterText.text = $"Clues Found: {cluesFound}/{clues.Length}";
    }
}
