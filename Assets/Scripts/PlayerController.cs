using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private int boxesCollected;
    public UnityEvent collectBoxEvent = new UnityEvent(); // For other scripts to invoke/call
    public TextMeshProUGUI repairText;

    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    public GameObject box4;
    private void Start()
    {
        boxesCollected = 0; // Initialise here otherwise this gets the wrong value
    }

    public void CollectBox()
    {
        boxesCollected++;
        repairText.text = "Spaceship Repairs: " + boxesCollected + "/4";
        Debug.Log("Boxes collected " + boxesCollected);

        //    if (boxToDestroy == box1)
        //    {
        //        Destory(box1);
        //    }
        //    else if (boxToDestroy == box2) {
        //        Destroy(box2);
        //}
    }

}
