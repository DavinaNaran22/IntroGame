using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class to keep track of spaceship parts:
// Whether they can be instantiated and position where they instantiate
// As well as keeping track of what scene they can spawn in

// Allows class to show in unity editor
[System.Serializable]
public class SpawnBox
{
    public GameObject BoxToSpawn;
    public Vector3 SpawnPosition;
    public string SceneSpawnIn;
    public bool BoxDestroyed = false;

    public SpawnBox(GameObject boxToSpawn, Vector3 spawnPosition, string sceneSpawnIn)
    {
        BoxToSpawn = boxToSpawn;
        SpawnPosition = spawnPosition;
        SceneSpawnIn = sceneSpawnIn;
    }

    public bool CanInstantiate()
    {
        // Return t/f depending on if current scene is scene it can spawn in and if it has (not) been destroyed
        return SceneManager.GetActiveScene().name.Equals(SceneSpawnIn) && !BoxDestroyed;
    }

    public void UpdateBoxDetails(Vector3 newSpawnPoint, string newScene)
    {
        SceneSpawnIn = newScene;
        SpawnPosition = newSpawnPoint;
        Debug.Log("New spawn point of this box in " + SceneSpawnIn + " is " + SpawnPosition);
    }
}
