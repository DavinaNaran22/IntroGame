using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public CinemachineThirdPersonAim cameraFollow;
    public GameObject HUD;
    public GameObject progressButton;
    public GameObject inventoryManager;
    public GameObject inventoryCanvas;
    public GameObject hovertext;
    public GameObject minimapCanvas;
    public GameObject subtitles;
    
    
    // Disable cinemachine camera so player can get control back
    // Enable progress button and hovertext on screen
    public void EndCamera()
    {
        Debug.Log("EndCamera signal recieved");
        cameraFollow.gameObject.SetActive(false);
        progressButton.SetActive(true);
        hovertext.SetActive(true);
        inventoryManager.SetActive(true);
    }

    // Hide all non-cutscene elements
    public void stopHUD()
    {
        HUD.SetActive(false);
        inventoryManager.SetActive(false);
        progressButton.SetActive(false);
        inventoryCanvas.SetActive(false);
        hovertext.SetActive(false);
        minimapCanvas.SetActive(false);
        subtitles.SetActive(false);
    }
}
