using System.Collections;
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

    public float maxWeatherSoundLevel = 0.8f;

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
                    if ( weatherData[i].particleSystem != null)
                    {
                        weatherData[i].emission.enabled = true;
                    }

                    weatherData[i].currentFogColor = RenderSettings.fogColor;
                    RenderSettings.fogColor = Color.Lerp(weatherData[i].fogColor, weatherData[i].currentFogColor, Time.deltaTime);
                    RenderSettings.fogDensity = weatherData[i].fogDensity;
                    ChangeWeatherSettings(weatherData[i].lightIntensity, weatherData[i].weatherAudio, weatherData[i].audioFadeTimer);

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

    private void ChangeWeatherSettings(float lightIntensity, AudioClip audioClip, float fadeTimer)
    {
        AdjustLightIntensity(lightIntensity);

        AudioSource tmpAudio = GetComponent<AudioSource>();

        if ((tmpAudio.clip != null) & ((audioClip == null) | (tmpAudio.clip != audioClip)))
        {
            TurnVolumeDown(fadeTimer);
        }

        if ((audioClip != null) & (tmpAudio.clip != audioClip))
        {
            ChangeAudioClip(audioClip);
        }

        if ((tmpAudio.clip != null) & (tmpAudio.clip == audioClip))
        {
            TurnVolumeUp(fadeTimer);
        }
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

    private void TurnVolumeDown(float fadeTimer)
    {
        AudioSource tmpAudio = GetComponent<AudioSource>();

        while (tmpAudio.volume > 0)
        {
            tmpAudio.volume -= fadeTimer * Time.deltaTime;
        }
    }

    private void TurnVolumeUp(float fadeTimer)
    {
        AudioSource tmpAudio = GetComponent<AudioSource>();
        while (tmpAudio.volume < maxWeatherSoundLevel)
        {
            tmpAudio.volume += fadeTimer * Time.deltaTime;
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