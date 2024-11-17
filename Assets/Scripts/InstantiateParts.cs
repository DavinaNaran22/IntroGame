using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantiateParts : Singleton<InstantiateParts>
{
    [SerializeField] private GameObject SpaceshipPart;
    [SerializeField] private Vector3 boxPos1 = new Vector3(417.003632f, 0.730000019f, 416.523956f);
    [SerializeField] private Vector3 boxPos2 = new Vector3(477.021759f, 0.505999982f, 425.860657f);
    [SerializeField] private Vector3 boxPos3 = new Vector3(441.538483f, 1.87427521f, 466.338531f);
    [SerializeField] private Vector3 boxPos4 = new Vector3(381.53833f, 0.280617714f, 406.347992f);
    private Boolean hasAddedParts = false;

    private void Update()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "landscape" && !hasAddedParts)
        {
            AddPart(boxPos1);
            AddPart(boxPos2);
            AddPart(boxPos3);
            AddPart(boxPos4);
            hasAddedParts = true;
        }
        if (activeScene.name != "landscape")
        {
            hasAddedParts = false;
        }
    }

    // Instantiate box @ vector
    private void AddPart(Vector3 boxPos)
    {
        GameObject box = Instantiate(SpaceshipPart, boxPos, Quaternion.identity); 
    }
}
