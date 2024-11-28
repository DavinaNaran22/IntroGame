using System;
using TMPro;
using UnityEngine;

public class PlayerNearText : MonoBehaviour
{
    [SerializeField] private string Text;
    [SerializeField] private float sphereRadius = 3f;
    private TextMeshProUGUI hoverText;
    private bool modifyingText = false;

    private void Start()
    {
        hoverText = GameObject.FindWithTag("HoverText").GetComponent<TextMeshProUGUI>();
    }

    // Return true/false depending on whether player is near this game object
    private bool PlayerIsNear()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.name == "Player")
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        // If the player is near this game object

        if (PlayerIsNear())
        {
            // And the current value of the next is nothing
            // Then change it
            if (hoverText.text.Length == 0)
            {
                hoverText.text = Text;
                modifyingText = true;
            }
        }
        // Only get rid of text if the game object attached to the script IS the one modifying it
        else if (modifyingText && hoverText.text.Length != 0)
        {
            hoverText.text = "";
            modifyingText = false;
        }
    }




}