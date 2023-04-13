using System;
using System.Collections;
using System.Collections.Generic;
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
    public float minLightIntencity = 0f;
    public float maxLightIntencity = 1f;

    public Light sunLight;
    public Transform windzone;

    public Weather weatherState;
    public WeatherData[] weatherData;

    public void Awake()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0f;
    }

    public void Start()
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

    IEnumerator  StartWeather()
    {
        ActivateWeather(((Weather)weatherState).ToString());
        yield return null;
    }

    public void RestartWeather()
    {
        if (StartWeather()!= null)
        {
            StopCoroutine(StartWeather());
        }

        StartCoroutine(StartWeather());
    }

    public void ActivateWeather(string weather)
    {
        Debug.Log("Loading " + weather + " weather");

        if (weatherData.Length > 0)
        {
            for (int i = 0; i < weatherData.Length; i++)
            {
                if (weatherData[i].particleSystem != null)
                {
                    if (weatherData[i].name == weather)
                    {
                        weatherData[i].emission.enabled = true;
                        weatherData[i].fogColor = RenderSettings.fogColor;
                        RenderSettings.fogColor = Color.Lerp(weatherData[i].currentFogColor, weatherData[i].fogColor, Time.deltaTime);
                        ChangeLighting(weatherData[i].lightIntencity);
                    }
                }
            }
        }
    }

    void ChangeLighting(float lightIntencity)
    {
        Light tmpLight = GetComponent<Light>();
        if (tmpLight.intensity > maxLightIntencity) 
        { tmpLight.intensity -= lightIntencity * Time.deltaTime; }

        if (tmpLight.intensity < minLightIntencity) 
        { tmpLight.intensity += lightIntencity * Time.deltaTime; }
    }
}
