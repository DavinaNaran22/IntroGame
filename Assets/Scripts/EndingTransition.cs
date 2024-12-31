using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTransition : MonoBehaviour
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

        StartCoroutine(LoadAsyncScene());

    }

    IEnumerator LoadAsyncScene()
    {
        yield return StartCoroutine(Fade(1f));
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        SceneManager.sceneLoaded += OnSceneLoad;
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

    // When scene loaded, move player to spawn point and spawn boxes
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        //PartsManager = GameObject.FindWithTag("ShipPartManager").GetComponent<InstantiateParts>();
        //PartsManager.CanSpawn();
        player.transform.position = spawnPoint;
        player.transform.rotation = (Quaternion.Euler(0, 0, 0));
        // So that text from one scene doesn't carry over from another
        GameManager.Instance.hoverText.text = "";

        StartCoroutine(Fade(0f));

    }

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
