using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This could be moved to another class which changes material colours etc.
public enum ColourMode
{
    NoColourBlindness,
    Protanopia,
    Deuteranopia,
    Tritanopia
}

[Serializable]
class PlayerData
{
    // Data which should be saved - add whatever's relevant
    public float health;
    public bool unlockedDoor;
    public bool playFirstCutscene;
    public ColourMode colorMode;
    public string currentScene;
    public Difficulty difficulty;
    public float volume;
    public float mouseSens;
    public float xPos; // Cant serialize unity specific things e.g. Vector3
    public float yPos;
    public float zPos;

    public bool interiorTaskOne;
    public bool interiorTaskTwo;
    public bool complTaskOne;
    public bool complTaskTwo;
    public bool complTaskThree;
    public bool complTaskFour;
    public bool puzzleCompleted;
    public bool complTaskFive;
    public bool completedSceneFive;

    public int medicineCount;
    public int clueCount;
    public int herbsCount;

    public float currentProgress;
    public string currentTask;
    public string progressText;
}

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    public GameObject player;
    public float playerHealth;
    public GameObject PlayerCanvas;
    public float xPos;
    public float yPos;
    public float zPos;

    [Header("Interior")]
    public bool unlockedDoor;
    public bool playFirstCutscene;
    public double CutsceneTime = 0;

    [Header("Options UI")]
    public ColourMode colourMode;
    public TMP_Dropdown colourDropdown;
    public ColourDropdown colourScript;
    public TMP_Dropdown difficultyDropdown;
    public Difficulty Difficulty;
    public float Volume;
    public float MouseSens;
    public float GameTime = 1;

    [Header("Inventory Prefabs")]
    public GameObject gunPrefab;
    public GameObject knifePrefab;
    public GameObject SwordPrefab;

    public GameObject MinimapCanvas;

    [Header("Miscellaneous State")]
    public string CurrentScene;
    public TextMeshProUGUI hoverText;
    public GameObject UIManager;
    public GameObject cameraCanvas;
    public CameraManagement cameraManagement;
    public TextMeshProUGUI cameraMsg;
    public GameObject Weather;
    public TaskManager taskManager;
    public bool boostedSwordCrafted = false;
    public bool task4Completed = false;
    public ShowClue ShowClueScript;

    [Header("Progress")]
    public float currentProgress;
    public string currentTask;
    public string progressText;

    [Header("Game Ending")]
    public bool triggerEnding = false;
    public bool shownClue = false;
    public static bool StartClueActive = false;
    //public static bool ShowClueActive = false;
    public bool canEnableShowClue = false;

    [Header("Task Progress")]
    public bool interiorTaskOne = false;
    public bool interiorTaskTwo = false;
    public bool completedTaskOne = false;
    public bool completedTaskTwo = false;
    public bool completedTaskThree = false;
    public bool completedTaskFour = false;
    public bool puzzleCompleted = false;
    public bool completedTaskFive = false;
    public bool completedSceneFive = false;

    [Header("State Managers")]
    public EquipManager equipManager;
    public QuantitySaveManager quantitySaveManager;

    [Header("Quantity State")]
    public int medicineCount = 0;
    public int clueCount = 0;
    public int herbsCount = 0;

    private void Start()
    {
        // The following are values chagned by pause menu
        // If player edits default values at start
        // Then the values have to be updated like this
        GameObject optionsManager = GameObject.Find("OptionsManager");
        colourScript = optionsManager.GetComponent<ColourDropdown>();
        colourMode = colourScript.mode;
        Difficulty = optionsManager.GetComponent<DifficultyDropdown>().difficulty;
        optionsManager.GetComponent<VolumeSlider>().AudioMixer.GetFloat("volume", out Volume);
        MouseSens = MouseLook.mouseSensitivity;
        GameTime = optionsManager.GetComponent<GameSpeedSlider>().GameTime;
    }

    private void Update()
    {
        FindDropdowns();

        if (triggerEnding && SceneManager.GetActiveScene().name == "Interior")
        {
            ActivateEnding();
        }

        ToggleWeather();
    }

    // Dropdowns aren't null in Main scene when in view
    private void FindDropdowns()
    {
        if (colourDropdown == null)
        {
            GameObject gameObject = GameObject.FindWithTag("ColourDropdown");
            if (gameObject != null)
            {
                colourDropdown = gameObject.GetComponent<TMP_Dropdown>();
            }
        }
        if (difficultyDropdown == null)
        {
            GameObject gameObject = GameObject.FindWithTag("DifficultyDropdown");
            if (gameObject != null)
            {
                difficultyDropdown = gameObject.GetComponent<TMP_Dropdown>();
            }
        }
    }

    private void ToggleWeather()
    {
        if (SceneManager.GetActiveScene().name == "landscape")
        {
            Weather.SetActive(true); // Activate the GameObject if in Landscape scene
        }
        else
        {
            Weather.SetActive(false); // Deactivate if not in Landscape
        }
    }

    // Save() and Load() are from Resource 10.1 Data Persistance on QMPlus
    // https://qmplus.qmul.ac.uk/pluginfile.php/3476919/mod_resource/content/0/Resource%2010.1%20Data%20Persistance.pdf
    public void Save()
    {
        string filename = Application.persistentDataPath + "/playInfo.dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filename, FileMode.OpenOrCreate);

        PlayerData data = new PlayerData();
        data.unlockedDoor = unlockedDoor;
        data.playFirstCutscene = playFirstCutscene;
        data.health = playerHealth;
        data.colorMode = colourMode;
        data.currentScene = GetCurrentScene();
        data.difficulty = Difficulty;
        data.volume = Volume;
        data.mouseSens = MouseSens;
        data.xPos = player.transform.position.x;
        data.yPos = player.transform.position.y;
        data.zPos = player.transform.position.z;
        data.interiorTaskOne = interiorTaskOne;
        data.interiorTaskTwo = interiorTaskTwo;
        data.complTaskOne = completedTaskOne;
        data.complTaskTwo = completedTaskTwo;
        data.complTaskThree = completedTaskThree;
        data.puzzleCompleted = puzzleCompleted;
        data.complTaskFour = completedTaskFour;
        data.complTaskFive = completedTaskFive;
        data.completedSceneFive = completedSceneFive;
        data.medicineCount = medicineCount;
        data.clueCount = clueCount;
        data.herbsCount = herbsCount;
        data.progressText = progressText;
        data.currentTask = currentTask;
        data.currentProgress = currentProgress;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Player progess saved at " + filename);

        equipManager.Save();
        quantitySaveManager.Save();
    }

    public void Load()
    {
        string filename = Application.persistentDataPath + "/playInfo.dat";
        if (File.Exists(filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filename, FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            unlockedDoor = data.unlockedDoor;
            playFirstCutscene = data.playFirstCutscene;
            playerHealth = data.health;
            colourMode = data.colorMode;
            CurrentScene = data.currentScene;
            Difficulty = data.difficulty;
            Volume = data.volume;
            MouseSens = data.mouseSens;
            interiorTaskOne = data.interiorTaskOne;
            interiorTaskTwo = data.interiorTaskTwo;
            completedTaskOne = data.complTaskOne;
            completedTaskTwo = data.complTaskTwo;
            completedTaskThree = data.complTaskThree;
            completedTaskFour = data.complTaskFour;
            puzzleCompleted = data.puzzleCompleted;
            completedTaskFive = data.complTaskFive;
            completedSceneFive = data.completedSceneFive;
            medicineCount = data.medicineCount;
            herbsCount = data.herbsCount;
            clueCount = data.clueCount;
            progressText = data.progressText;
            currentProgress = data.currentProgress;
            currentTask = data.currentTask;

            // Load scene and spawn player in saved position
            SceneManager.LoadScene(CurrentScene);
            SceneManager.sceneLoaded += OnSavedSceneLoad;
            Debug.Log("Loaded Player data");
        }
        else
        {
            Debug.Log("No file with player data at location " + filename + " so no loading of player data");
        }

        equipManager.Load();
        quantitySaveManager.Load();
        taskManager.LoadProgress();
    }

    private void OnSavedSceneLoad(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = new Vector3(xPos, yPos, zPos);
        player.transform.rotation = (Quaternion.Euler(0, 0, 0));
        hoverText.text = "";
    }

    private string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ActivateEnding()
    {
        GameObject.Find("EndingScene").GetComponent<Ending>().enabled = true;
        Debug.Log("Enabled ending game object");
    }
}