using TMPro;
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
            if (hoverGO != null)
            {
                hoverText = hoverGO.GetComponent<TextMeshProUGUI>();
            }
        }

        // If the player is near this game object and hover text is visible
        if (PlayerIsNear() && gameObject.activeSelf)
        {
            // And the current value of the text is nothing
            // Then change it
            if (hoverText.text.Length == 0)
            {
                hoverText.text = Text;
                modifyingText = true;
            }
        }
        // Only get rid of text if the game object attached to the script IS the one modifying it
        else if (modifyingText && hoverText.text == Text)
        {
            ClearHoverText();
        }
    }

    private void OnDisable()
    {
        // Clear hover text if this object becomes inactive
        if (modifyingText && hoverText != null && hoverText.text == Text)
        {
            ClearHoverText();
        }
    }

    private void ClearHoverText()
    {
        hoverText.text = "";
        modifyingText = false;
    }
}
