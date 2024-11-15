using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveTransition: MonoBehaviour
{
    public string scene;
    public Vector3 spawnPos;
    private Boolean sceneNotLoaded = true;
    //when player enters cave switches scene
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && sceneNotLoaded) // Scene might get loaded mult times because of collider position
        {
            StartCoroutine(LoadAsyncScene());
            sceneNotLoaded = false;
            other.transform.position = spawnPos;
        }
    }

    IEnumerator LoadAsyncScene()
    {
        Scene currScene = SceneManager.GetActiveScene();

        // Load new scene at the same time as current scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move player and ui to new scene
        SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("Player"), SceneManager.GetSceneByName(scene));
        SceneManager.MoveGameObjectToScene(GameObject.FindWithTag("UIManager"), SceneManager.GetSceneByName(scene));

        //currScene.GetRootGameObjects.

        // Unload old scene
        SceneManager.UnloadSceneAsync(currScene);
    }
}
