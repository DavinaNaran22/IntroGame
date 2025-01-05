using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    private void Update()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == "Death" || activeScene == "Win")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameObject.SetActive(false);
        }
    }
}
