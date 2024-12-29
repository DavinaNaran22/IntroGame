using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPlay : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] bool AddEventListener = true;
    // Start is called before the first frame update
    void Start()
    {
        if (AddEventListener) playButton.onClick.AddListener(GoCurrentScene);
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
