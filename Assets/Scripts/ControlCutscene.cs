using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class ControlCutscene : MonoBehaviour
{
    [SerializeField] PlayableDirector PlayableDirector;
    [SerializeField] CinemachineThirdPersonAim thirdPersonCam;
    [SerializeField] GameObject deadAstro1;
    [SerializeField] GameObject deadAstro2;
    [SerializeField] GameObject cutsceneAstro1;
    [SerializeField] GameObject cutsceneAstro2;

    private void Start()
    {
        // Only play cutscene if bool in game manager is true - in theory this should only happen once
        if (GameManager.Instance.playFirstCutscene)
        {
            HideDeadAstros();
            PlayableDirector.Play();
        }
        else
        {
            thirdPersonCam.gameObject.SetActive(false);
            ShowDeadAstros();
        }
    }

    private void OnEnable()
    {
        PlayableDirector.stopped += OnPlayableDirectorStopped;
    }

    private void OnPlayableDirectorStopped(PlayableDirector playableDirector)
    {
        GameManager.Instance.playFirstCutscene = false;
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
}
