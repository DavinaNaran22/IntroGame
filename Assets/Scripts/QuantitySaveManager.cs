using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Need to keep track of which items player has equipped
// And therefore show the correct tiles in the inventory
[Serializable]
public class QuantityData
{
    public string tileName;
    public bool isActive;

    public QuantityData(GameObject obj, bool isActive)
    {
        this.tileName = obj.name;
        this.isActive = isActive;
    }
}

// Cant serialize list
[Serializable]
public class QuantityListSerializable
{
    public List<QuantityData> data;

    public QuantityListSerializable(List<QuantityData> data)
    {
        this.data = data;
    }

    public List<QuantityData> GetList() { return this.data; }
}

public class QuantitySaveManager : MonoBehaviour
{
    public List<QuantityData> tileList = new List<QuantityData>();
    [SerializeField] QuantityManager quantityManager;

    public void UpdateActiveStatus(GameObject obj, bool status)
    {
        foreach (var item in tileList)
        {
            if (item.tileName == obj.name)
            {
                item.isActive = status;
            }
        }
    }

    // Used when loading in QuantityManager
    public bool GetActiveStatus(GameObject obj)
    {
        foreach (var item in tileList)
        {
            if (item.tileName == obj.name)
            {
                return item.isActive;
            }
        }
        return false; // Just in case not in list
    }

    public void Save()
    {
        QuantityListSerializable serializableList = new QuantityListSerializable(tileList);
        string filename = Application.persistentDataPath + "/quantityInfo.json";
        string json = JsonUtility.ToJson(serializableList);

        File.WriteAllText(filename, json);
        Debug.Log("Quantity list saved at " + filename);
    }

    public void Load()
    {
        string filename = Application.persistentDataPath + "/quantityInfo.json";
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            QuantityListSerializable list = JsonUtility.FromJson<QuantityListSerializable>(json);
            tileList = list.GetList();

            quantityManager.LoadSavedData();

            Debug.Log("Loaded quantity data");
        }
        else
        {
            Debug.Log("No file with quantity data at location " + filename + " so no loading of quantity data");
        }
    }
}
