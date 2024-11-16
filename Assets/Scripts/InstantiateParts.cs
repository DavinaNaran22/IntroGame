using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateParts : MonoBehaviour
{
    [SerializeField] private GameObject SpaceshipPart;
    [SerializeField] private Vector3 boxPos1 = new Vector3(417.003632f, 0.730000019f, 416.523956f);
    [SerializeField] private Vector3 boxPos2;
    [SerializeField] private Vector3 boxPos3;
    [SerializeField] private Vector3 boxPos4;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(SpaceshipPart, boxPos1, Quaternion.identity);
        Instantiate(SpaceshipPart, boxPos2, Quaternion.identity);
        Instantiate(SpaceshipPart, boxPos3, Quaternion.identity);
        Instantiate(SpaceshipPart, boxPos4, Quaternion.identity);
    }

}
