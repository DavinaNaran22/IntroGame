using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private int boxesCollected;
    public UnityEvent collectBoxEvent = new UnityEvent();
    public TextMeshProUGUI repairText;
    public int testing;

    private void Start()
    {

        boxesCollected = 0;
    }

    public void CollectBox()
    {
        boxesCollected++;
        repairText.text = "Spaceship Repairs: " + boxesCollected + "/4";
        Debug.Log("Boxes collected " + boxesCollected); // https://www.youtube.com/watch?v=djW7g6Bnyrc
    }

}
