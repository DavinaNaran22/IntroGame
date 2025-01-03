using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartClue : MonoBehaviour
{
    public TextMeshProUGUI repairText;
    public GameObject clue;
    public GameObject repairTask4;
    public GameObject repairTask5;
    public TaskManager taskManager;


    private void Start()
    {
        taskManager = GameManager.Instance.taskManager;
        StartCoroutine(returnTextShow());
    }

    private IEnumerator returnTextShow()
    {
        repairText.gameObject.SetActive(true);
        taskManager.IncreaseProgress(9);
        taskManager.SetTaskText("Piece clues together");
        yield return new WaitForSeconds(2f);
        repairText.gameObject.SetActive(false);
        repairTask4.SetActive(false);
        clue.SetActive(true);
        repairTask5.SetActive(true);

    }

}

