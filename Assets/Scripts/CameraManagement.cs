using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraManagement : MonoBehaviour
{
    public GameObject uiElements;       // Parent GameObject containing all UI elements
    public GameObject cameraFrame;      // Camera frame for photo mode
    public Transform logContent;        // Content object in the Log tab's Scroll View
    public GameObject photoEntryPrefab; // Prefab for photo entries
    public GameObject photoViewer;      // Fullscreen panel for viewing expanded photos
    public RawImage photoViewerImage;   // RawImage component inside the photo viewer
    public Camera mainCamera;           // Player's POV camera
    public Camera PictureCam;           // Camera for taking pictures

    private bool isPhotoModeActive = false;
    private string screenshotFolder = "Screenshots"; // Folder to save screenshots

    void Start()
    {
        uiElements.SetActive(true);
        cameraFrame.SetActive(false);
    }

    void Update()
    {
        // Toggle photo mode when 'P' is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePhotoMode();
        }

        // Exit photo mode when 'Esc' is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && isPhotoModeActive)
        {
            ExitPhotoMode();
        }

        // Take a screenshot when 'T' is pressed
        if (Input.GetKeyDown(KeyCode.T) && isPhotoModeActive)
        {
            TakeScreenshot();
        }
    }

    void TogglePhotoMode()
    {
        isPhotoModeActive = true;
        uiElements.SetActive(false);     // Hide all UI elements
        cameraFrame.SetActive(true);     // Show the frame panel

        // Align pictureCam with mainCamera
        PictureCam.transform.position = mainCamera.transform.position;
        PictureCam.transform.rotation = mainCamera.transform.rotation;
        PictureCam.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }

    void ExitPhotoMode()
    {
        isPhotoModeActive = false;
        uiElements.SetActive(true);      // Show all UI elements
        cameraFrame.SetActive(false);    // Hide the frame panel

        mainCamera.transform.position = PictureCam.transform.position;
        mainCamera.transform.rotation = PictureCam.transform.rotation;
        mainCamera.gameObject.SetActive(true);
        PictureCam.gameObject.SetActive(false);


    }

    void TakeScreenshot()
    {
        StartCoroutine(CaptureScreenshot());
    }

    IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        // Get camera frame dimensions
        RectTransform frameRect = cameraFrame.GetComponent<RectTransform>();
        Rect screenRect = GetScreenRect(frameRect);

        // Create texture for the screenshot
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

    Rect GetScreenRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        float x = corners[0].x;
        float y = corners[0].y;
        float width = corners[2].x - corners[0].x;
        float height = corners[2].y - corners[0].y;
        return new Rect(x, y, width, height);
    }

    void AddPhotoToLog(Texture2D photo)
    {
        GameObject newEntry = Instantiate(photoEntryPrefab, logContent);

        // Set the photo in the RawImage
        RawImage rawImage = newEntry.GetComponentInChildren<RawImage>();
        rawImage.texture = photo;

        // Assign functionality to Expand and Delete buttons
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
