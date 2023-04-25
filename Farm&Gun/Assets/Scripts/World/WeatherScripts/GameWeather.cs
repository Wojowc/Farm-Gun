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
    public float weatherVolume = 0.6f;
    public float audioFader = 0.0001f;
    public float fogFadeTimer = 0.0001f;
    public AudioSource audioSource;
    public GameObject clouds;
    private Material cloudsMaterial;
    public float windSpeed = 1f;
    public Weather weatherState;
    public WeatherData[] weatherData;
    public Color tempFogColor;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0.0f;

        clouds = GameObject.Find("CloudsPlane");
        cloudsMaterial = clouds.GetComponent<MeshRenderer>().materials[0];
    }

    private void Start()
    {
        LoadWeatherSystem();
        if (weatherState == 0)
        {
            StartCoroutine(StartWeather());
        }
        cloudsMaterial.SetFloat("_AlphaClip", 1);
        cloudsMaterial.SetFloat("_Cutoff", 0.9f);
        cloudsMaterial.EnableKeyword("_ALPHATEST_ON");
    }

    private void Update()
    {
        cloudsMaterial.mainTextureOffset += new Vector2(0, windSpeed / 1000 * Time.deltaTime);
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
                    cloudsMaterial.SetFloat("_Cutoff", weatherData[i].cloudsThreshold);
                    StartCoroutine(ChangeFog(weatherData[i].fogColor, weatherData[i].fogDensity));
                    ChangeAudio(weatherData[i].weatherAudio);
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
            progress += (fogFadeTimer);
        }

        yield return null;
    }

    private void ChangeAudio(AudioClip audioClip)
    {
        AudioSource tmpAudio = GetComponent<AudioSource>();

        if ((tmpAudio.clip != null) & ((audioClip == null) | (tmpAudio.clip != audioClip)))
        {
            StartCoroutine(TurnVolumeDown());
        }

        if ((audioClip != null) & (tmpAudio.clip != audioClip))
        {
            ChangeAudioClip(audioClip);
        }

        if ((tmpAudio.clip != null) & (tmpAudio.clip == audioClip))
        {
            StartCoroutine(TurnVolumeUp());
        }
    }

    //private void AdjustLightIntensity(float lightIntensity)
    //{
    //    Light tmpLight = GetComponent<Light>();

    //    if (tmpLight.intensity > maxLightIntensity)
    //    {
    //        tmpLight.intensity -= lightIntensity * Time.deltaTime;
    //    }

    //    if (tmpLight.intensity < minLightIntensity)
    //    {
    //        tmpLight.intensity += lightIntensity * Time.deltaTime;
    //    }
    //}

    private IEnumerator TurnVolumeDown()
    {
        AudioSource tmpAudio = GetComponent<AudioSource>();

        while (tmpAudio.volume > 0)
        {
            tmpAudio.volume -= audioFader;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    private IEnumerator TurnVolumeUp()
    {
        AudioSource tmpAudio = GetComponent<AudioSource>();
        while (tmpAudio.volume < weatherVolume)
        {
            tmpAudio.volume += audioFader;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
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