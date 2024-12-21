using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class CameraManagement : Singleton<CameraManagement>
{
    // UI Elements
    public GameObject uiElements;       // Parent GameObject containing all UI elements
    public GameObject cameraFrame;      // Overlay frame for photo mode
    public Transform logContent;        // Log content for storing captured screenshots
    public GameObject photoEntryPrefab; // Prefab for photo entries
    public GameObject photoViewer;      // Panel for viewing expanded photos
    public RawImage photoViewerImage;   // RawImage for displaying the expanded photo
    public GameObject cameraModel;

    // Cameras
    public Camera mainCamera;           // Main camera for gameplay
    public Camera pictureCam;           // Camera for photo-taking mode

    // Frame Detection
    public RectTransform borderFrame;   // RectTransform of the frame image
    public List<GameObject> targetObjects; // List of GameObjects to check within the frame

    // Misc
    private bool isPhotoModeActive = false; // Tracks if photo mode is active
    private string screenshotFolder = "Screenshots"; // Folder for saving screenshots
    public float maxDistance = 25; // Maximum allowed distance between the player and the target

    public TextMeshProUGUI cameraCloserMsg;
    public TextMeshProUGUI cameraTakenMsg;

    public GameObject HUD;

    // Input system
    private PlayerInputActions inputActions;

    private bool isCapturingScreenshot = false;


    // New input system for taking photos and dismissing dialogue
    private new void Awake()
    {
        inputActions = new PlayerInputActions();
        base.Awake(); // Setup singleton
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.OpenCamera.performed += ctx => TogglePhotoMode();
        inputActions.Player.TakePhoto.performed += ctx => TakeScreenshot();
        inputActions.Player.ExitCamera.performed += ctx => ExitPhotoMode();

    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.OpenCamera.performed -= ctx => TogglePhotoMode();
        inputActions.Player.TakePhoto.performed -= ctx => TakeScreenshot();
        inputActions.Player.ExitCamera.performed -= ctx => ExitPhotoMode();

    }

    void Start()
    {
        // Initialize UI state
        uiElements.SetActive(true);
        cameraFrame.SetActive(false);
        pictureCam.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "landscape")
        {

            targetObjects.Clear();
            GameObject greenAlien1 = GameObject.Find("GreenAlien1");
            GameObject greenAlien2 = GameObject.Find("GreenAlien2");

            if (greenAlien1 != null && greenAlien1.activeInHierarchy &&
            greenAlien2 != null && greenAlien2.activeInHierarchy)
            {
                // Add GreenAlien1 and GreenAlien2 to targetObjects if they are active
                targetObjects.Add(greenAlien1);
                targetObjects.Add(greenAlien2);
            }
            else
            {
                // If GreenAlien1 and GreenAlien2 are not active, focus on GreenAlien3
                GameObject greenAlien3 = GameObject.Find("GreenAlien3");
                if (greenAlien3 != null)
                {
                    targetObjects.Add(greenAlien3);
                }
                else
                {
                    GameObject greenAlien4 = GameObject.Find("GreenAlien4");
                    if (greenAlien4 != null)
                    {
                        targetObjects.Add(greenAlien4);
                    }
                }
            }

        }
        else if (SceneManager.GetActiveScene().name == "CaveScene")
        {
            if (targetObjects.Count != 1 || targetObjects[0] == null || targetObjects[0].name != "Rhinoceros")
            {
                targetObjects.Clear();
                targetObjects.Add(GameObject.Find("Rhinoceros"));
            }
        }
    }


    public void TogglePhotoMode()
    {
        if (cameraModel != null && cameraModel.activeSelf)
        {
            Debug.Log("Cannot enter photo mode because CameraModel is active.");
            return; // Exit the function
        }
        isPhotoModeActive = true;

        // Transition to photo mode
        uiElements.SetActive(false);
        cameraFrame.SetActive(true);
        AlignCamera();
        mainCamera.gameObject.SetActive(false);
        pictureCam.gameObject.SetActive(true);
        HUD.SetActive(false);
    }

    public void ExitPhotoMode()
    {
        isPhotoModeActive = false;

        // Transition back to gameplay
        uiElements.SetActive(true);
        cameraFrame.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        pictureCam.gameObject.SetActive(false);
        cameraCloserMsg.gameObject.SetActive(false);
        cameraTakenMsg.gameObject.SetActive(false);
        HUD.SetActive(true);
    }

    public void AlignCamera()
    {
        // Align the pictureCam with the mainCamera
        pictureCam.transform.position = mainCamera.transform.position;
        pictureCam.transform.rotation = mainCamera.transform.rotation;
    }

    public void TakeScreenshot()
    {
        if (isCapturingScreenshot) return;  // Prevent multiple calls

        if (IsAnyTargetInFrame())
        {
            Debug.Log("Target detected inside the frame. Capturing screenshot...");
            StartCoroutine(CaptureScreenshot());
        }
        else
        {
            Debug.Log("No targets within the frame. Screenshot not taken.");
        }
    }

    public bool IsAnyTargetInFrame()
    {
        foreach (GameObject target in targetObjects)
        {
            if (target != null && IsWithinFrame(target) && IsWithinDistance(target))
            {
                cameraCloserMsg.gameObject.SetActive(false);
                cameraTakenMsg.gameObject.SetActive(true);

                isPhotoModeActive = false;

                StartCoroutine(TransitionBackToGameplay());

                return true;
            }
        }
        return false;
    }

    private IEnumerator TransitionBackToGameplay()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(1.5f);

        // Transition back to gameplay
        uiElements.SetActive(true);
        cameraFrame.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        pictureCam.gameObject.SetActive(false);
        cameraCloserMsg.gameObject.SetActive(false);
        cameraTakenMsg.gameObject.SetActive(false);
        HUD.SetActive(true);
        yield return new WaitForSeconds(1.5f);
    }

    public bool IsWithinDistance(GameObject target)
    {
        float distance = Vector3.Distance(mainCamera.transform.position, target.transform.position);
        Debug.Log($"Distance to {target.name}: {distance}");
        cameraCloserMsg.gameObject.SetActive(true);
        return distance <= maxDistance;
    }


    public bool IsWithinFrame(GameObject target)
    {
        // Convert target position to screen space
        Vector3 screenPoint = pictureCam.WorldToScreenPoint(target.transform.position);

        // Ensure the target is in front of the camera
        if (screenPoint.z < 0) return false;

        // Get screen space corners of the frame
        Vector3[] corners = new Vector3[4];
        borderFrame.GetWorldCorners(corners);
        Rect screenRect = new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);

        // Check if the target's screen position falls inside the frame
        return screenRect.Contains(new Vector2(screenPoint.x, screenPoint.y));
    }

    public IEnumerator CaptureScreenshot()
    {
        isCapturingScreenshot = true; // Set the flag
        yield return new WaitForEndOfFrame();

        // Calculate frame dimensions in screen space
        Vector3[] corners = new Vector3[4];
        borderFrame.GetWorldCorners(corners);
        Rect screenRect = new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);

        // Capture the screenshot texture
        Texture2D screenshot = new Texture2D((int)screenRect.width, (int)screenRect.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(screenRect, 0, 0);
        screenshot.Apply();

        // Save the screenshot
        string folderPath = Path.Combine(Application.persistentDataPath, screenshotFolder);
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        string fileName = Path.Combine(folderPath, $"Screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss}.png");
        File.WriteAllBytes(fileName, screenshot.EncodeToPNG());
        Debug.Log($"Screenshot saved to: {fileName}");

        // Add the screenshot to the log
        AddPhotoToLog(screenshot);

        isCapturingScreenshot = false; // Reset the flag
    }

    public void AddPhotoToLog(Texture2D photo)
    {
        // Create a new log entry
        GameObject newEntry = Instantiate(photoEntryPrefab, logContent);

        // Assign the screenshot to the RawImage
        RawImage rawImage = newEntry.GetComponentInChildren<RawImage>();
        rawImage.texture = photo;

        // Set up buttons for expanding and deleting the photo
        Button expandButton = newEntry.transform.Find("ExpandButton").GetComponent<Button>();
        expandButton.onClick.AddListener(() => ExpandPhoto(photo));

        Button deleteButton = newEntry.transform.Find("DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(() => DeletePhoto(newEntry));
    }

    public void ExpandPhoto(Texture2D photo)
    {
        photoViewer.SetActive(true);
        photoViewerImage.texture = photo;
    }

    public void ClosePhotoViewer()
    {
        photoViewer.SetActive(false);
    }

    void DeletePhoto(GameObject photoEntry)
    {
        Destroy(photoEntry);
    }
}
