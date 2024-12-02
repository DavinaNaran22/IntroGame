using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CutsceneController : MonoBehaviour
{
    public CinemachineThirdPersonAim cameraFollow;

    //Disable cinemachine camera so player can get control back
    public void EndCamera()
    {
        cameraFollow.gameObject.SetActive(false);
    }
}
