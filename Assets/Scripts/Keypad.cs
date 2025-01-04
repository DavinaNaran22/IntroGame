using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : FindPlayerTransform
{
    public TaskManager taskManager;
    [SerializeField] TMP_InputField keypadInput;
    [SerializeField] KeypadDoor doorScript;
    private const string CODE = "2836";
    private PlayerInputActions inputActions;
    private MouseLook mouseLook;
    // TMP input is weird so this is a workaround to compare text/length
    // https://discussions.unity.com/t/textmesh-pro-ugui-hidden-characters/683388/47
    private int inputLength = 0;
    private string actualInput = "";

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
        GetPlayerTransform();
        mouseLook = Player.GetComponentInChildren<MouseLook>(); // Attached to main camera
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.CloseInventory.performed += ctx => ExitUI();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.CloseInventory.performed -= ctx => ExitUI();
    }

    // Called in button.OnClick()
    public void ButtonClick(Button button)
    {
        string buttonText = button.GetComponentInChildren<TextMeshProUGUI>().text;
        // Append input to input field if current input length is less than length of code
        if (inputLength < CODE.Length)
        {
            keypadInput.text += buttonText;
            actualInput += buttonText;
            inputLength += 1;
        }
    }

    public void ClearInput()
    {
        keypadInput.text = "";
        inputLength = 0;
        actualInput = "";
    }

    public void EnterInput()
    {
        // Check if user input is same as code
        if (actualInput == CODE)
        {
            Debug.Log("CORRECT CODE");
            GameManager.Instance.unlockedDoor = true;
            taskManager.IncreaseProgress(7); // Increase progress by 7%
            taskManager.SetTaskText("Find missing thruster");
            GameManager.Instance.hoverText.text = "Right click to leave ship";
            ExitUI();
        }
    }

    private void ExitUI()
    {
        doorScript.keypadShowing = false;
        GameManager.Instance.player.GetComponent<PlayerMovement>().ToggleMovement();
        mouseLook.canLook = true;
        this.gameObject.SetActive(false);
    }
}
