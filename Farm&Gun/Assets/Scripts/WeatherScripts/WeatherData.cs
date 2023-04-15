using UnityEngine;

[System.Serializable]
public struct WeatherData
{
    public string name;
    public ParticleSystem particleSystem;

   // [HideInInspector]
    public ParticleSystem.EmissionModule emission;

    public bool useAudio;
    public AudioClip weatherAudio;
    public float audioFadeTimer;

    public float lightIntensity;
    public float lightDimTimer;
    public float fogChangeSpeed;

    public Color fogColor;
    public Color currentFogColor;
    public float fogDensity;
}