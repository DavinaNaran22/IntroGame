using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveToEnding : MonoBehaviour
{
    
    [SerializeField] private string scene;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;
    private GameObject player;
    //private InstantiateParts PartsManager;
    protected bool hasCheckCondition = false;

    // When player enters trigger switch scene (and if no check condition)
    void Start()
    {
        GameManager.Instance.triggerEnding = true;
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        yield return StartCoroutine(Fade(1f));
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        SceneManager.sceneLoaded += OnSceneLoad;
        GameManager.Instance.triggerEnding = true;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    protected void LoadOtherScene(GameObject Player)
    {
        player = Player;
        StartCoroutine(LoadAsyncScene());
        GameManager.Instance.hoverText.text = "";
    }

    // When scene loaded, move player to spawn point 
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.player.transform.position = spawnPoint;
        GameManager.Instance.player.transform.rotation = (Quaternion.Euler(0, 0, 0));
        // So that text from one scene doesn't carry over from another
        GameManager.Instance.hoverText.text = "";
        // Activate EndingScene game object in interior
        GameManager.Instance.triggerEnding = true;

        StartCoroutine(Fade(0f));

    }

    // Fade in and out
    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = targetAlpha;
    }
}


