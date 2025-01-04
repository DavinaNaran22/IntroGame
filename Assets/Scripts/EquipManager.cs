using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class EquipData
{
    public string objectName;
    // Use object position to tell if object has been equipped or not
    // Cant serialize Vector3
    public float x;
    public float y;
    public float z;
    public bool hasBeenEquiped = false;

    public EquipData(GameObject obj, bool hasBeenEquiped)
    {
        this.objectName = obj.name;
        this.x = obj.transform.position.x;
        this.y = obj.transform.position.y;
        this.z = obj.transform.position.z;
        this.hasBeenEquiped = hasBeenEquiped;
    }
}

// Cant get json of list of serializable objects
// https://stackoverflow.com/questions/41787091/unity-c-sharp-jsonutility-is-not-serializing-a-list
[Serializable]
public class EquipDataListSerializable
{
    //public List<EquipData> list = new List<EquipData>();
    public List<EquipData> list;

    public EquipDataListSerializable(List<EquipData> list)
    {
        this.list = list;
    }

    public List<EquipData> getList()
    {
        return list;
    }
}

public class EquipManager : Singleton<EquipManager>
{
    public List<EquipData> equipObjects = new List<EquipData>();

    public EquipData GetFromEquipList(GameObject gameObject)
    {
        foreach (var equip in equipObjects)
        {
            if (new Vector3(equip.x, equip.y, equip.z) == gameObject.transform.position) return equip;
        }
        return null; // Change to something better if possible
}

    public void Save()
    {
        Debug.Log("In Equip Save");
        EquipDataListSerializable serializableList = new EquipDataListSerializable(equipObjects);
        string filename = Application.persistentDataPath + "/equipInfo.json";
        string json = JsonUtility.ToJson(serializableList);

        Debug.Log("Json");
        Debug.Log(json);
        File.WriteAllText(filename, json);

        Debug.Log("Equip list saved at " + filename);
    }

    public void Load()
    {
        string filename = Application.persistentDataPath + "/equipInfo.json";
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            EquipDataListSerializable list = JsonUtility.FromJson<EquipDataListSerializable>(json);
            equipObjects = list.getList();

            Debug.Log("Loaded equip data");
        }
        else
        {
            Debug.Log("No file with equip data at location " + filename + " so no loading of equip data");
        }
    }
}
