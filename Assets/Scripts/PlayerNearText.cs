using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerNearText : MonoBehaviour
{
    public string Text;
    [SerializeField] private float sphereRadius = 3f;
    public TextMeshProUGUI hoverText;
    private bool modifyingText = false;

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
        // Would happen if player moves out of scene this object is in
        if (hoverText == null)
        {
            GameObject hoverGO = GameObject.FindWithTag("HoverText");
            if (hoverGO != null) {
                hoverText = hoverGO.GetComponent<TextMeshProUGUI>();
            }
        }

        // If the player is near this game object and hover text is visible
        if (PlayerIsNear())
        {
            // And the current value of the next is nothing
            // Then change it
            if (hoverText.text.Length == 0)
            {
                hoverText.enabled = true;
                StartCoroutine(message_active(5f));
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

    IEnumerator message_active(float delay)
    {
        yield return new WaitForSeconds(delay);

        hoverText.enabled = false;
    }
}
