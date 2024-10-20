using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private int boxesCollected;
    public UnityEvent collectBoxEvent = new UnityEvent(); // For other scripts to call
    public TextMeshProUGUI repairText;
    public int testing;

    private void Start()
    {
        boxesCollected = 0; // Have to assign value in Start() otherwise value gets overwritten
    }

    public void CollectBox() // Can be invoked by other scripts
    {
        boxesCollected++;
        repairText.text = "Spaceship Repairs: " + boxesCollected + "/4"; // Update text to having new value
        Debug.Log("Boxes collected " + boxesCollected);
    }

}
