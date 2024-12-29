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
    [SerializeField] GameObject subtitleBox;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject OxygenBar;
    public static double cutsceneLength = 120;

    private void Start()
    {        
        if (GameManager.Instance.playFirstCutscene)
        {
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
        thirdPersonCam.gameObject.SetActive(false);
        subtitleBox.SetActive(false);
        ShowDeadAstros();
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
        // Stop cutscene from playing (even if they go to menu before cutscene finished)
        GameManager.Instance.playFirstCutscene = false;
        HUD.SetActive(true);
        HealthBar.SetActive(true);
        OxygenBar.SetActive(true);
        ShowDeadAstros();
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
