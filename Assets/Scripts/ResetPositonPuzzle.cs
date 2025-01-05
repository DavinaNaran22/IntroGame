using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetPositionPuzzle : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private Vector3 Cockpit_Position = new Vector3(567.340027f, 8.90999985f, 505.584991f);
    //private InstantiateParts PartsManager;
    protected bool hasCheckCondition = false;
    private GameObject player;


    // When player enters trigger switch scene (and if no check condition)
    void Update()
    {
        if (Puzzle.Puzzle_Complete == true)
        {
            StartCoroutine(LoadAsyncScene());

            //SceneManager.LoadScene(scene);
            //SceneManager.sceneLoaded += OnSceneLoad;
        }
    }

    IEnumerator LoadAsyncScene()
    {
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


    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
      
        //GameManager.Instance.PlayerCanvas.SetActive(true);
        //GameManager.Instance.player.SetActive(true);
        player.transform.position = Cockpit_Position;
        Debug.Log("COCKPIT");
        // So that text from one scene doesn't carry over from another
        GameManager.Instance.hoverText.text = "";
    }
}
