using UnityEngine;
using UnityEngine.Playables;

public class ControlCutscene : MonoBehaviour
{
    [SerializeField] PlayableDirector PlayableDirector;
    private void Start()
    {
        // Only play cutscene if bool in game manager is true - in theory this should only happen once
        if (GameManager.Instance.playFirstCutscene)
        {
            PlayableDirector.Play();
        }
    }

    private void OnEnable()
    {
        PlayableDirector.stopped += OnPlayableDirectorStopped;
    }

    private void OnPlayableDirectorStopped(PlayableDirector playableDirector)
    {
        GameManager.Instance.playFirstCutscene = false;
    }
}
