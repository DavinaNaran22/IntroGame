using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Debug.Log("Player");
        Debug.Log(Player);
        Player.transform.position = transform.position; // Move player to position of this go (i.e. spawn point)
        Debug.Log("Player Transform ");
        Debug.Log(Player.transform.position);
    }
}
