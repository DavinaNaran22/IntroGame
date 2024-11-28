using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    [SerializeField] TMP_InputField keypadInput;
    private const string CODE = "2836";
    // TMP input is weird so this is a workaround to compare text/length
    // https://discussions.unity.com/t/textmesh-pro-ugui-hidden-characters/683388/47
    private int inputLength = 0;
    private string actualInput = "";


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
        }
    }
}
