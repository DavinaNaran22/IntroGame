using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeatherManager : MonoBehaviour
{
    public RainController rainController;
    public FogController fogController; 
    public LightningController lightningController;
    public GameObject weather; 
    public float minWeatherInterval = 30f;  // Minimum time between weather changes
    public float maxWeatherInterval = 120f; // Maximum time between weather changes

    private void Start()
    {
        StartCoroutine(WeatherCycle());
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "landscape")
        {
            weather.SetActive(true); // Activate the GameObject if in Landscape scene
        }
        else
        {
            weather.SetActive(false); // Deactivate if not in Landscape
        }
    }

    IEnumerator WeatherCycle()
    {
        while (true)
        {
            // Randomly decide the type of weather
            int weatherType = Random.Range(0, 3); // 0 = Light Rain, 1 = Heavy Rain, 2 = No Rain

            if (weatherType == 0)
            {
                Debug.Log("Light Rain Starting");
                rainController.StartLightRain();
                fogController.DecreaseFogDensity(); // Clear/light fog with light rain
                lightningController.enabled = false; // Disable lightning
            }
            else if (weatherType == 1)
            {
                Debug.Log("Heavy Rain Starting");
                rainController.StartHeavyRain();
                fogController.IncreaseFogDensity(); // Heavy fog with heavy rain
                lightningController.enabled = true; // Enable lightning
            }
            else
            {
                Debug.Log("No Rain");
                rainController.StopRain();
                fogController.DecreaseFogDensity(); // Clear fog
                lightningController.enabled = false; // Disable lightning
            }

            // Wait for the duration of this weather before changing
            float interval = Random.Range(minWeatherInterval, maxWeatherInterval);
            yield return new WaitForSeconds(interval);

            // Stop any active effects before starting the next cycle
            rainController.StopRain();
            fogController.DecreaseFogDensity();
            lightningController.enabled = false;
        }
    }
}
