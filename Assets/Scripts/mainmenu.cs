using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Works in editor
        #endif
        Application.Quit();
    }

    public void Restart()
    {
        // Move dont destory objects back to original scenes
        // Need to re add dont destory back

        // Dont destory script
        // List of dont destory objects

        Debug.Log("Restarting game");
        // Assign everything correct values
        // Load main scene
        SceneManager.LoadScene("Main");
    }
}