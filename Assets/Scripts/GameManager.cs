using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

[Serializable]
class PlayerData
{
    // Data which should be saved - add whatever's relevant
    public int health;
    public bool unlockedDoor;
    public bool playFirstCutscene;
}

public class GameManager : Singleton<GameManager>
{
    public bool unlockedDoor;
    public GameObject player;
    public TextMeshProUGUI hoverText;
    public bool playFirstCutscene;

    // Save() and Load() are from Resource 10.1 Data Persistance on QMPlus
    // https://qmplus.qmul.ac.uk/pluginfile.php/3476919/mod_resource/content/0/Resource%2010.1%20Data%20Persistance.pdf
    public void Save()
    {
        string filename = Application.persistentDataPath + "/playInfo.data";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filename, FileMode.OpenOrCreate);

        PlayerData data = new PlayerData();
        data.unlockedDoor = unlockedDoor;
        data.playFirstCutscene = playFirstCutscene;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Player progess saved");
    }

    public void Load()
    {
        string filename = Application.persistentDataPath + "/playInfo.dat";
        if (File.Exists(filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filename, FileMode.Open);

            PlayerData data = (PlayerData) bf.Deserialize(file);
            file.Close();

            unlockedDoor = data.unlockedDoor;
            playFirstCutscene = data.playFirstCutscene;
            Debug.Log("Loading Player data");
        }
    }
}

