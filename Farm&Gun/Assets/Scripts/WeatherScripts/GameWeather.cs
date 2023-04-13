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
    public float minLightIntensity = 0f;
    public float maxLightIntensity = 1f;

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
        StartCoroutine(StartWeather());
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
                if (weatherData[i].particleSystem != null)
                {
                    if (weatherData[i].name == weather)
                    {
                        weatherData[i].emission.enabled = true;
                        weatherData[i].currentFogColor = RenderSettings.fogColor;
                        RenderSettings.fogColor = Color.Lerp(weatherData[i].fogColor, weatherData[i].currentFogColor, Time.deltaTime);
                        RenderSettings.fogDensity = weatherData[i].fogDensity;
                        StartCoroutine(ChangeWeatherSettings(weatherData[i].lightIntensity, weatherData[i].weatherAudio, weatherData[i].audioFadeInTimer));
                    }
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

    private IEnumerator ChangeWeatherSettings(float lightIntensity, AudioClip audioClip, float fadeTimer) //TODO: to be fixed
    {

        bool finished = false;
        while (!finished)
        {
            Light tmpLight = GetComponent<Light>();
            AudioSource tmpAudio = GetComponent<AudioSource>();

            if (tmpLight.intensity > maxLightIntensity)
            {
                tmpLight.intensity -= lightIntensity * Time.deltaTime;
            }

            if (tmpLight.intensity < minLightIntensity)
            {
                tmpLight.intensity += lightIntensity * Time.deltaTime;
            }

            if (tmpAudio.volume > 0 && tmpAudio.clip != audioClip)
            {
                tmpAudio.volume -= fadeTimer * Time.deltaTime;
            }

            if (tmpAudio.volume < 1 && tmpAudio.clip == audioClip)
            {
                tmpAudio.volume += fadeTimer * Time.deltaTime;
            }

            if (tmpAudio.volume >= 1 && tmpAudio.clip == audioClip)
            {
                finished = true;
                yield break;
            }

            if (tmpAudio.volume <= 0)
            {
                tmpAudio.Stop();
                tmpAudio.clip = audioClip;
                tmpAudio.loop = true;
                tmpAudio.Play();
            }
        }

        yield return null;
    }
}