using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoAlienBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform shootingPoint;
    public float detectionRadius = 5f;
    public Animator animator;
    public PlayerEquipment playerEquipment;

    public GameObject shotPrefab;
    public float shootRate = 0.5f;
    private float m_shootRateTimeStamp;

    private bool playerNearby = false;
    private bool isDead = false; // Flag to check if the alien is dead
    private bool isHit = false;

    
}
