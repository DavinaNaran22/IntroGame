using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Used to keep gameobject between scene loading

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }
    // when the game object is enabled link the method to check if game scene to the unity event 
    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckScene;
    }
    // when the game object is enabled unlink the method to check if game scene to the unity event
    private void OnDisable()
    { 
        SceneManager.sceneLoaded -= CheckScene;
    }
    // if the scene loaded is the "Game" scene destroy the object 
    private void CheckScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            Destroy(gameObject);
        }
    }
}
