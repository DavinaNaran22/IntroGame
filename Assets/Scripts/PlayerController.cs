using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerController : Singleton<PlayerController>
{
    private int boxesCollected;
    public UnityEvent collectBoxEvent = new UnityEvent(); // For other scripts to invoke/call
    public TextMeshProUGUI repairText;

    private void Start()
    {
        boxesCollected = 0; // Initialise here otherwise this gets the wrong value
    }

    public void CollectBox()
    {
        boxesCollected++;
        if (repairText != null)
        {
            repairText.text = "Spaceship Repairs: " + boxesCollected + "/4";
        }
        Debug.Log("Boxes collected " + boxesCollected);
    }

}
