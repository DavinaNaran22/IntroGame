using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquipData
{
    public string objectName;
    public Vector3 objPos; // Use object position to tell if object has been equipped or not
    public bool hasBeenEquiped = false;

    public EquipData(GameObject obj, bool hasBeenEquiped)
    {
        this.objectName = obj.name;
        this.objPos = obj.transform.position;
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
            if (equip.objPos == gameObject.transform.position) return equip;
        }
        return null; // Change to something better if possible
}

    //public void Save()
    //{

    //}
}
