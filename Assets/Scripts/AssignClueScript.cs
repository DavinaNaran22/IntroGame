using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignClueScript : MonoBehaviour
{
    [SerializeField] GameObject task2;
    [SerializeField] GameObject task3;
    [SerializeField] GameObject task4;
    [SerializeField] GameObject rt4clue;
    [SerializeField] GameObject StartClue;
    [SerializeField] GameObject task5;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ShowClueScript = GameObject.Find("ShowClue").GetComponent<ShowClue>();
        if (GameManager.Instance.canEnableShowClue) GameManager.Instance.ShowClueScript.enabled = true;
        
        if (GameManager.Instance.complTaskOne && !GameManager.Instance.complTaskTwo)
        {
            task2.SetActive(true);
        }

        if (GameManager.Instance.complTaskTwo && !GameManager.Instance.complTaskThree)
        {
            task3.SetActive(true);
        }

        if (GameManager.Instance.complTaskThree && !GameManager.Instance.complTaskFour)
        {
            task4.SetActive(true);
        }

        if (GameManager.Instance.complTaskFour)
        {
            task4.SetActive(false);
            if (!GameManager.Instance.puzzleStarted)
            {
                rt4clue.SetActive(true);
                StartClue.SetActive(true);
            } 
            else if (!GameManager.Instance.complTaskFive)
            {
                task5.SetActive(true);
            }
        }
    }
}
