using System.Collections;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public enum Weather
{
    Reserved0 = 0, //default weather condition
    Reserved1 = 1,
    Thunderstorm = 2,
    Drizzle = 3,
    Reserved4 = 4,
    Rain = 5,
    Snow = 6,
    Atmosphere = 7, //eg. fog
    Clear = 800,
    Clouds25 = 801,
    Clouds50 = 802,
    Clouds75 = 803,
    Clouds100 = 804,
}

public class GameWeather : MonoBehaviour
{
    //public float minLightIntensity = 0f;
    //public float maxLightIntensity = 1f;

    public float weatherVolume = 0.5f;
    public float audioFadeTimer = 0.00001f;
    public float fogFadeTimer = 0.00001f;

    public Color tempFogColor;

    public Light sunLight;
    public AudioSource audioSource;
    public Transform windZone;

    public Weather weatherState;
    public WeatherData[] weatherData;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0.0f;
    }

    private void Start()
    {
        LoadWeatherSystem();
        if (weatherState == 0)
        {
            StartCoroutine(StartWeather());
        }
    }

    private void LoadWeatherSystem()
    {
        for (int i = 0; i < weatherData.Length; i++)
        {
            weatherData[i].emission = weatherData[i].particleSystem.emission;
        }
    }

    private IEnumerator StartWeather()
    {
        ActivateWeather(((Weather)weatherState).ToString());
        yield break;
    }

    public void RestartWeather()
    {
        if (StartWeather() != null)
        {
            StopCoroutine(StartWeather());
        }

        StartCoroutine(StartWeather());
    }

    private void ActivateWeather(string weather)
    {
        Debug.Log("Loading " + weather + " weather");
        ResetWeather();

        if (weatherData.Length > 0)
        {
            for (int i = 0; i < weatherData.Length; i++)
            {
                if (weatherData[i].name == weather)
                {
                    if (weatherData[i].particleSystem != null)
                    {
                        weatherData[i].emission.enabled = true;
                    }

                    if (weatherData[i].lightSettings != null)
                    {
                        sunLight = weatherData[i].lightSettings;
                    }

                    StartCoroutine(ChangeFog(weatherData[i].fogColor, weatherData[i].fogDensity));
                    StartCoroutine(ChangeAudio(weatherData[i].weatherAudio));
                }
            }
        }
    }

    private void ResetWeather()
    {
        if (weatherData.Length > 0)
        {
            for (int i = 0; i < weatherData.Length; i++)
            {
                if (weatherData[i].emission.enabled != false)
                    weatherData[i].emission.enabled = false;
            }
        }
    }

    private IEnumerator ChangeFog(Color targetColor, float fogDensity)
    {
        RenderSettings.fogDensity = fogDensity;

        float progress = 0f;
        while (progress < 1)
        {
            RenderSettings.fogColor = Color.Lerp(tempFogColor, targetColor, progress);
            progress += (fogFadeTimer);// * Time.deltaTime);
        }

        yield return null;
    }

    private IEnumerator ChangeAudio(AudioClip audioClip)
    {
        AudioSource tmpAudio = GetComponent<AudioSource>();

        if ((tmpAudio.clip != null) & ((audioClip == null) | (tmpAudio.clip != audioClip)))
        {
            TurnVolumeDown();
        }

        if ((audioClip != null) & (tmpAudio.clip != audioClip))
        {
            ChangeAudioClip(audioClip);
        }

        if ((tmpAudio.clip != null) & (tmpAudio.clip == audioClip))
        {
            TurnVolumeUp();
        }

        yield return null;
    }

    private void AdjustLightIntensity(float lightIntensity)
    {
        //Light tmpLight = GetComponent<Light>();

        //if (tmpLight.intensity > maxLightIntensity)
        //{
        //    tmpLight.intensity -= lightIntensity * Time.deltaTime;
        //}

        //if (tmpLight.intensity < minLightIntensity)
        //{
        //    tmpLight.intensity += lightIntensity * Time.deltaTime;
        //}
    }

    private void TurnVolumeDown()
    {
        AudioSource tmpAudio = GetComponent<AudioSource>();

        while (tmpAudio.volume > 0)
        {
            tmpAudio.volume -= audioFadeTimer * Time.deltaTime;
        }
    }

    private void TurnVolumeUp()
    {
        AudioSource tmpAudio = GetComponent<AudioSource>();
        while (tmpAudio.volume < weatherVolume)
        {
            tmpAudio.volume += audioFadeTimer * Time.deltaTime;
        }
    }

    private void ChangeAudioClip(AudioClip audioClip)
    {
        AudioSource tmpAudio = GetComponent<AudioSource>();
        tmpAudio.Stop();
        tmpAudio.clip = audioClip;
        tmpAudio.loop = true;
        tmpAudio.Play();
    }
}