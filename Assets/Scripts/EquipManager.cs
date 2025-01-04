using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipData
{
    public GameObject equipableObject;
    public bool hasBeenEquiped = false;

    public EquipData(GameObject equipableObject, bool hasBeenEquiped)
    {
        this.equipableObject = equipableObject;
        this.hasBeenEquiped = hasBeenEquiped;
    }
}

public class EquipManager : Singleton<EquipManager>
{
    public List<EquipData> equipObjects = new List<EquipData>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EquipData GetFromEquipList(GameObject gameObject)
    {
        foreach (var equip in equipObjects)
        {
            if (equip.equipableObject == gameObject) return equip;
        }
        return null; // Change to something better if possible
}

    //public void Save()
    //{

    //}
}
