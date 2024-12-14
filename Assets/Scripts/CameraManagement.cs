using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraManagement : MonoBehaviour
{
    // UI Elements
    public GameObject uiElements;       // Parent GameObject containing all UI elements
    public GameObject cameraFrame;      // Overlay frame for photo mode
    public Transform logContent;        // Log content for storing captured screenshots
    public GameObject photoEntryPrefab; // Prefab for photo entries
    public GameObject photoViewer;      // Panel for viewing expanded photos
    public RawImage photoViewerImage;   // RawImage for displaying the expanded photo

    // Cameras
    public Camera mainCamera;           // Main camera for gameplay
    public Camera pictureCam;           // Camera for photo-taking mode

    // Frame Detection
    public RectTransform borderFrame;   // RectTransform of the frame image
    public List<GameObject> targetObjects; // List of GameObjects to check within the frame

    // Misc
    private bool isPhotoModeActive = false; // Tracks if photo mode is active
    private string screenshotFolder = "Screenshots"; // Folder for saving screenshots
    public float maxDistance = 25f; // Maximum allowed distance between the player and the target


    void Start()
    {
        // Initialize UI state
        uiElements.SetActive(true);
        cameraFrame.SetActive(false);
        pictureCam.gameObject.SetActive(false);
    }

    void Update()
    {
        // Toggle photo mode with 'P'
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePhotoMode();
        }

        // Exit photo mode with 'Esc'
        if (Input.GetKeyDown(KeyCode.Escape) && isPhotoModeActive)
        {
            ExitPhotoMode();
        }

        // Capture a screenshot with 'T'
        if (Input.GetKeyDown(KeyCode.T) && isPhotoModeActive)
        {
            TakeScreenshot();
        }
    }

    void TogglePhotoMode()
    {
        isPhotoModeActive = true;

        // Transition to photo mode
        uiElements.SetActive(false);
        cameraFrame.SetActive(true);
        AlignCamera();
        mainCamera.gameObject.SetActive(false);
        pictureCam.gameObject.SetActive(true);
    }

    void ExitPhotoMode()
    {
        isPhotoModeActive = false;

        // Transition back to gameplay
        uiElements.SetActive(true);
        cameraFrame.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        pictureCam.gameObject.SetActive(false);
    }

    void AlignCamera()
    {
        // Align the pictureCam with the mainCamera
        pictureCam.transform.position = mainCamera.transform.position;
        pictureCam.transform.rotation = mainCamera.transform.rotation;
    }

    void TakeScreenshot()
    {
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

    bool IsAnyTargetInFrame()
    {
        foreach (GameObject target in targetObjects)
        {
            if (target != null && IsWithinFrame(target) && IsWithinDistance(target))
            {
                return true;
            }
        }
        return false;
    }

    bool IsWithinDistance(GameObject target)
    {
        float distance = Vector3.Distance(mainCamera.transform.position, target.transform.position);
        Debug.Log($"Distance to {target.name}: {distance}");
        return distance <= maxDistance;
    }


    bool IsWithinFrame(GameObject target)
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

    IEnumerator CaptureScreenshot()
    {
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
    }

    void AddPhotoToLog(Texture2D photo)
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

    void ExpandPhoto(Texture2D photo)
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
