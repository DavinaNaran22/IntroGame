using UnityEngine;

public class FogController : MonoBehaviour
{
    public float lightFogDensity = 0.004f;  // Fog density for light rain
    public float heavyFogDensity = 0.015f;  // Fog density for heavy rain
    public float fogTransitionTime = 19f; // Time to transition between fog densities

    private float currentFogDensity;

    void Start()
    {
        // Initialize fog settings
        RenderSettings.fog = true;
        RenderSettings.fogDensity = lightFogDensity; // Start with light fog
        currentFogDensity = lightFogDensity;
    }

    public void IncreaseFogDensity()
    {
        StopAllCoroutines(); // Stop any ongoing transitions
        StartCoroutine(TransitionFogDensity(heavyFogDensity));
    }

    public void DecreaseFogDensity()
    {
        StopAllCoroutines(); // Stop any ongoing transitions
        StartCoroutine(TransitionFogDensity(lightFogDensity));
    }

    private System.Collections.IEnumerator TransitionFogDensity(float targetDensity)
    {
        float elapsedTime = 0f;
        float initialDensity = RenderSettings.fogDensity;

        while (elapsedTime < fogTransitionTime)
        {
            elapsedTime += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(initialDensity, targetDensity, elapsedTime / fogTransitionTime);
            yield return null;
        }

        RenderSettings.fogDensity = targetDensity; // Ensure it ends at the exact target value
    }
}
