// https://youtu.be/C5FmpgnBRFE?si=F0uBo2cwpXnYAP6b

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    public GameObject lightning1;
    public GameObject lightning2;
    public GameObject lightning3;

    public GameObject audio1;

    // Make the lightning and audio not show initially
    private void Start()
    {
        lightning1.SetActive(false);
        lightning2.SetActive(false);
        lightning3.SetActive(false);
        audio1.SetActive(false);

        Invoke("CallLightning", 20.75f);
    }

    // Call the lightning, add time delays for flashing effect
    // Call the thunder sound after the last lightning
    void CallLightning()
    {
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            lightning1.SetActive(true);
            Invoke("EndLighning", 0.125f); // Slowest
            Invoke("CallThunder", 0.395f);
        }
        else if (r == 1)
        {
            lightning2.SetActive(true);
            Invoke("EndLighning", 0.105f);
            Invoke("CallThunder", 0.195f);
        }
        else
        {
            lightning3.SetActive(true);
            Invoke("EndLighning", 0.755f); // Fastest
            CallThunder();
        }

    }


    // End the lightning
    void EndLighning()
    {
        lightning1.SetActive(false);
        lightning2.SetActive(false);
        lightning3.SetActive(false);

        // Random time delay between lightnings
        float rand = Random.Range(37.5f, 78.7f);
        Invoke("CallLightning", rand);
    }

    // Call the thunder sound
    void CallThunder()
    {
        audio1.SetActive(true);
        Invoke("EndThunder", 10f);
    }


    // End the thunder sound
    void EndThunder()
    {
        audio1.SetActive(false);
    }

    public void EnableLightning()
    {
        enabled = true; // Resume lightning behavior
    }

    // Disable the lightning and audio
    public void DisableLightning()
    {
        enabled = false; // Stop lightning behavior
        lightning1.SetActive(false);
        lightning2.SetActive(false);
        lightning3.SetActive(false);
        audio1.SetActive(false);
        CancelInvoke(); // Cancel any scheduled lightning or thunder
    }



}
