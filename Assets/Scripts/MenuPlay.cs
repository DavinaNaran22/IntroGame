using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPlay : MonoBehaviour
{
    [SerializeField] Button playButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(GoCurrentScene);
    }

    // Load the current scene, or start game if no current scene
    void GoCurrentScene()
    {
        // Should GameManager current scene be able to be null?
        if (GameManager.Instance == null || GameManager.Instance.CurrentScene == null)
        {
            SceneManager.LoadScene("Interior");
        } else
        {
            if (GameManager.Instance.CurrentScene == "Interior")
            {
                Debug.Log("Menu play interior test");
                Debug.Log(GameManager.Instance.playFirstCutscene);
                Debug.Log(GameManager.Instance.CutsceneTime);
                // if went to main menu before finishing cutscene, start from beginning
                // easiest thing to implement
                if (GameManager.Instance.playFirstCutscene && GameManager.Instance.CutsceneTime < ControlCutscene.cutsceneLength)
                {
                    GameManager.Instance.CutsceneTime = 0;
                }
            }
            SceneManager.LoadScene(GameManager.Instance.CurrentScene);
        }
    }
}
