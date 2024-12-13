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
            SceneManager.LoadScene(GameManager.Instance.CurrentScene);
        }
    }
}
