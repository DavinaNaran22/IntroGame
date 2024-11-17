using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneTransition: MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private Vector3 spawnPoint;
    private GameObject player;

    //when player enters trigger switch scene
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            SceneManager.LoadScene(scene);
            SceneManager.sceneLoaded += OnSceneLoad;
        }
    }

    // When scene loaded, move player to spawn point
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = spawnPoint;
    }
}
