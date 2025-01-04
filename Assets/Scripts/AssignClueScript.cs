using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To keep track of which tasks have been completed or not
// And that the right ones start when the player moves to landscape
public class AssignClueScript : MonoBehaviour
{
    [SerializeField] GameObject task2;
    [SerializeField] GameObject task3;
    [SerializeField] GameObject task4;
    [SerializeField] GameObject rt4clue;
    [SerializeField] GameObject cavePlane;
    [SerializeField] GameObject StartClue;
    [SerializeField] GameObject task5;
    void Start()
    {
        GameManager.Instance.ShowClueScript = GameObject.Find("ShowClue").GetComponent<ShowClue>();
        if (GameManager.Instance.canEnableShowClue) GameManager.Instance.ShowClueScript.enabled = true;
        
        if (GameManager.Instance.completedTaskOne && !GameManager.Instance.completedTaskTwo)
        {
            task2.SetActive(true);
        } 
        else if (GameManager.Instance.completedTaskTwo && !GameManager.Instance.completedTaskThree)
        {
            task3.SetActive(true);
        }
        else if (GameManager.Instance.completedTaskThree && !GameManager.Instance.completedTaskFour)
        {
            task4.SetActive(true);
        }
        else if (GameManager.Instance.completedTaskFour)
        {
            task4.SetActive(false);
            cavePlane.SetActive(true);
            if (!GameManager.Instance.puzzleCompleted)
            {
                rt4clue.SetActive(true);
                StartClue.SetActive(true);
            } 
            else if (!GameManager.Instance.completedTaskFive)
            {
                task5.SetActive(true);
            }
        }
    }
}
