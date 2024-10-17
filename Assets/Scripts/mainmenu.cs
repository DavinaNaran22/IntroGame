using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    // works when the application is built, does not work in unity as using editor
    public void quit()
    {
        Application.Quit();
    }
}