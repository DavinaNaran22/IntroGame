using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public CinemachineThirdPersonAim cameraFollow;




    // Disable cinemachine camera so player can get control back
    public void EndCamera()
    {
        cameraFollow.gameObject.SetActive(false);
    }
}
