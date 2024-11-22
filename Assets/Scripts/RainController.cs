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

    void Start()
    {
        // Start with light rain settings
        SetRainParameters(lightRainRate, lightRainSpeed);
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
            }
            else if (elapsedTime >= rainDuration)
            {
                // Gradually stop the rain
                StopRain();
            }
        }
    }

    void SetRainParameters(float emissionRate, float startSpeed)
    {
        var emission = rainParticleSystem.emission;
        var main = rainParticleSystem.main;

        // Set emission rate
        emission.rateOverTime = emissionRate;

        // Set start speed
        main.startSpeed = startSpeed;
    }

    void StopRain()
    {
        // Gradually reduce emission to zero
        var emission = rainParticleSystem.emission;
        emission.rateOverTime = Mathf.Lerp(emission.rateOverTime.constant, 0, Time.deltaTime);

        if (emission.rateOverTime.constant <= 0.1f)
        {
            isRaining = false;
            rainParticleSystem.Stop();
        }
    }
}
