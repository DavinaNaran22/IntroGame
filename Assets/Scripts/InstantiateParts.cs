using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantiateParts : Singleton<InstantiateParts>
{
    [SerializeField] SpawnBox box1;
    [SerializeField] SpawnBox box2;
    [SerializeField] SpawnBox box3;
    [SerializeField] SpawnBox box4;
    public bool canAddBoxes = true;

    // Can't add in Start() since it's only ever called once
    // This is a problem if a scenes is loaded multiple times
    // So parts are 'added' in Update()
    private void Update()
    {
        if (canAddBoxes)
        {
            AddPart(box1);
            AddPart(box2);
            AddPart(box3);
            AddPart(box4);
            canAddBoxes = false;
        }
    }

    // Instantiate box @ vector and assign it its spawn box
    private void AddPart(SpawnBox box)
    {
        if (box.CanInstantiate())
        {
            Debug.Log("Instantiating box at " + box.SpawnPosition);
            GameObject boxGO = Instantiate(box.BoxToSpawn, box.SpawnPosition, Quaternion.identity);
            boxGO.GetComponent<PickupBlock>().spawnBox = box;
            
        }
    }

    // Called when scene changes to allow boxes to spawn
    public void CanSpawn()
    {
        canAddBoxes = true;
    }
}
        
