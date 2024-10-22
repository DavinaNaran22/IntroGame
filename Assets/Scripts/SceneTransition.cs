using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    //A function to switch between scenes
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
