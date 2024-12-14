using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class ControlCutscene : MonoBehaviour
{
    [SerializeField] PlayableDirector pd;
    [SerializeField] CinemachineThirdPersonAim thirdPersonCam;
    [SerializeField] GameObject deadAstro1;
    [SerializeField] GameObject deadAstro2;
    [SerializeField] GameObject cutsceneAstro1;
    [SerializeField] GameObject cutsceneAstro2;
    public static double cutsceneLength = 120;
    private bool stoppedPlaying = false;

    private void Start()
    {
        //if (CanPlayCutScene())
        if (GameManager.Instance.playFirstCutscene)
        {
            Debug.Log("Can play cutscene");
            //Debug.Log();
            HideDeadAstros();
            pd.Play();
        }
        else
        {
            StopPlaying();
        }
    }

    private void StopPlaying()
    {
        stoppedPlaying = true;
        thirdPersonCam.gameObject.SetActive(false);
        ShowDeadAstros();
    }

    private void Update()
    {
        // Stop playing cutscene (e.g. if playFirstCutscene updated somewhere else)
        //if (!GameManager.Instance.playFirstCutscene && !stoppedPlaying)
        //{
        //    StopPlaying();
        //    stoppedPlaying = true;
        //}
        Debug.Log("Control cutscene");
        Debug.Log(GameManager.Instance.playFirstCutscene);
    }

    // Only play cutscene if bool in game manager is true - in theory this should only happen once
    bool CanPlayCutScene()
    {
        return GameManager.Instance.playFirstCutscene && GameManager.Instance.CutsceneTime < cutsceneLength;
    }

    private void OnEnable()
    {
        pd.stopped += OnPlayableDirectorStopped;
    }

    private void OnPlayableDirectorStopped(PlayableDirector playableDirector)
    {
        Debug.Log("on pd stop");
        Debug.Log(pd.time);
        Debug.Log(cutsceneLength);
        if (pd.time >= cutsceneLength)
        {
            GameManager.Instance.playFirstCutscene = false;
            ShowDeadAstros();
        }
    }

    private void ShowDeadAstros()
    {
        deadAstro1.SetActive(true);
        deadAstro2.SetActive(true);
        // Hide astros used in cutscene
        cutsceneAstro1.SetActive(false);
        cutsceneAstro2.SetActive(false);
    }

    private void HideDeadAstros()
    {
        deadAstro1.SetActive(false);
        deadAstro2.SetActive(false);
    }

    // Set details about cutscene in GameManager
    public void SetCutsceneDetails()
    {
        GameManager.Instance.CutsceneTime = pd.time;
    }
}
