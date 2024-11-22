using UnityEngine;

public class RainController : MonoBehaviour
{
    public ParticleSystem rainParticleSystem;
    public float lightRainRate = 10f; // Emission rate for light rain
    public float heavyRainRate = 200f; // Emission rate for heavy rain
    public float lightRainSpeed = 5f; // Start speed for light rain
    public float heavyRainSpeed = 15f; // Start speed for heavy rain
    public float transitionTime = 40f; // Time to transition between light and heavy rain
    public float rainDuration = 60f; // Time the rain lasts before stopping

    private float elapsedTime = 0f;
    private bool isRaining = true;
    public FogController fogController; // Reference to the FogController


    //void Start()
    //{
    //    // Start with light rain settings
    //    SetRainParameters(lightRainRate, lightRainSpeed);
    //}

    void Start()
    {
        // Automatically find the ParticleSystem if not assigned
        if (rainParticleSystem == null)
        {
            rainParticleSystem = GetComponentInChildren<ParticleSystem>();
        }

        // Optionally start with light rain for testing
        StartLightRain();
    }

    public void StartLightRain()
    {
        elapsedTime = 0f;
        isRaining = true;
        rainParticleSystem.Play();
        SetRainParameters(lightRainRate, lightRainSpeed);
    }

    public void StartHeavyRain()
    {
        elapsedTime = 0f;
        isRaining = true;
        rainParticleSystem.Play();
        SetRainParameters(heavyRainRate, heavyRainSpeed);
    }

    public void StopRain()
    {
        isRaining = false;
        var emission = rainParticleSystem.emission;
        emission.rateOverTime = 0;
        rainParticleSystem.Stop();
    }

    void SetRainParameters(float emissionRate, float startSpeed)
    {
        var emission = rainParticleSystem.emission;
        var main = rainParticleSystem.main;

        // Adjust emission rate and particle speed
        emission.rateOverTime = emissionRate;
        main.startSpeed = startSpeed;
    }


    void Update()
    {
        if (isRaining)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime <= transitionTime)
            {
                // Gradually increase rain intensity
                float t = elapsedTime / transitionTime;
                float emissionRate = Mathf.Lerp(lightRainRate, heavyRainRate, t);
                float startSpeed = Mathf.Lerp(lightRainSpeed, heavyRainSpeed, t);

                SetRainParameters(emissionRate, startSpeed);

                if (fogController != null)
                {
                    fogController.IncreaseFogDensity();
                }
            }
            else if (elapsedTime >= rainDuration)
            {
                // Gradually stop the rain
                StopRain();
                if (fogController != null)
                {
                    fogController.DecreaseFogDensity();
                }
            }
        }


        if (elapsedTime <= transitionTime)
        {
            // Gradually increase rain intensity
            float t = elapsedTime / transitionTime;
            float emissionRate = Mathf.Lerp(lightRainRate, heavyRainRate, t);
            float startSpeed = Mathf.Lerp(lightRainSpeed, heavyRainSpeed, t);

            SetRainParameters(emissionRate, startSpeed);

            // Increase fog density as rain intensifies
            fogController.IncreaseFogDensity();
        }
        else if (elapsedTime >= rainDuration)
        {
            // Gradually stop the rain and reduce fog density
            StopRain();
            fogController.DecreaseFogDensity();
        }

    }


    //void StopRain()
    //{
    //    // Gradually reduce emission to zero
    //    var emission = rainParticleSystem.emission;
    //    emission.rateOverTime = Mathf.Lerp(emission.rateOverTime.constant, 0, Time.deltaTime);

    //    if (emission.rateOverTime.constant <= 0.1f)
    //    {
    //        isRaining = false;
    //        rainParticleSystem.Stop();
    //    }
    //}
}
