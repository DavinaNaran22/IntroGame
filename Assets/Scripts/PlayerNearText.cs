using TMPro;
using UnityEngine;

public class PlayerNearText : MonoBehaviour
{
    public string Text;
    [SerializeField] private float sphereRadius = 3f;
    protected TextMeshProUGUI hoverText;
    protected bool modifyingText = false;

    // Return true/false depending on whether the player is near this game object
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

    protected void Update()
    {
        LocateHoverText();
        ToggleText();
    }

    // Locate hoverText if it's null
    private void LocateHoverText()
    {
        if (hoverText == null)
        {
            // Navigate through the hierarchy to find hoverText inside a folder
            GameObject parentObject = GameObject.Find("UIManager"); // Replace with actual parent name
            if (parentObject != null)
            {
                Transform folderTransform = parentObject.transform.Find("PlayerCanvas"); // Replace with actual folder name
                if (folderTransform != null)
                {
                    GameObject hoverGO = folderTransform.Find("HoverText")?.gameObject; // Replace with hover text object name
                    if (hoverGO != null)
                    {
                        hoverText = hoverGO.GetComponent<TextMeshProUGUI>();
                    }
                }
            }
        }
    }

    private void ToggleText()
    {
        // If the player is near this game object and hover text is visible
        if (PlayerIsNear() && gameObject.activeSelf)
        {
            ShowText();
        }
        // Only get rid of text if the game object attached to the script IS the one modifying it
        else if (modifyingText && hoverText != null && hoverText.text == Text)
        {
            ClearHoverText();
        }
    }

    private void ShowText()
    {
        // If the current value of the text is nothing
        // Then change it
        if (hoverText != null && hoverText.text.Length == 0)
        {
            hoverText.text = Text;
            modifyingText = true;
        }
    }

    protected void OnDisable()
    {
        // Clear hover text if this object becomes inactive
        if (modifyingText && hoverText != null && hoverText.text == Text)
        {
            ClearHoverText();
        }
    }

    protected void ClearHoverText()
    {
        if (hoverText != null)
        {
            hoverText.text = "";
        }
        modifyingText = false;
    }
}
