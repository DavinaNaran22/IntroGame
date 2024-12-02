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
    
    
    // Disable cinemachine camera so player can get control back
    // Enable progress button and hovertext on screen
    public void EndCamera()
    {
        cameraFollow.gameObject.SetActive(false);
        progressButton.SetActive(true);
        hovertext.SetActive(true);
        //GameManager.Instance.playFirstCutscene = false;
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
    }
}
