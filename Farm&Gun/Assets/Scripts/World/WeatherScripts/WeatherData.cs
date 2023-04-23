using UnityEngine;

[System.Serializable]
public struct WeatherData
{
    public string name;
    public ParticleSystem particleSystem;

    [HideInInspector]
    public ParticleSystem.EmissionModule emission;
    
    public AudioClip weatherAudio;
    public LightingSettings lightSettings;

    public float lightIntensity;

    public float fogDensity;
    public Color fogColor;
}