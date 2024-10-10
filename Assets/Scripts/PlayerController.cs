using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public int boxesCollected = 0;
    public UnityEvent collectBoxEvent = new UnityEvent();
    public TextMeshProUGUI repairText;

    public void CollectBox()
    {
        boxesCollected++;
        repairText.text = "Spaceship Repairs: " + boxesCollected + "/4";
        Debug.Log("Boxes collected " + boxesCollected); // https://www.youtube.com/watch?v=djW7g6Bnyrc
    }

}
