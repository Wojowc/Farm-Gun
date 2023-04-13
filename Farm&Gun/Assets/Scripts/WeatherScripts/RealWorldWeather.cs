﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class RealWorldWeather : MonoBehaviour
{
    /*
		In order to use this API, you need to register on the website.

		Source: https://openweathermap.org

		Request by city: api.openweathermap.org/data/2.5/weather?q={city id}&appid={your api key}
		Request by lat-long: api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={your api key}

		Api response docs: https://openweathermap.org/current
	*/

    public string apiKey = "3d573ab2ce591040e35f1011f55024ea";

    public string city = "";
    public bool useLatLng = false;
    public string latitude = "";
    public string longitude = "";
    public int truncatedWeatherId = 0;
    public float windSpeed = 0;

    private GameObject weatherSystem;

    public void Awake()
    {
        GetRealWeather();
    }

    public void GetRealWeather()
    {
        city = GameSettings.LocationCity;
        string uri = "api.openweathermap.org/data/2.5/weather?";
        if (useLatLng)
        {
            uri += "lat=" + latitude + "&lon=" + longitude + "&appid=" + apiKey;
        }
        else
        {
            uri += "q=" + city + "&appid=" + apiKey;
        }
        StartCoroutine(GetWeatherCoroutine(uri));
    }

    private IEnumerator GetWeatherCoroutine(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
#pragma warning disable 0618
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log("Web request error: " + webRequest.error);
            }
            else
            {
                ParseJson(webRequest.downloadHandler.text);
            }
#pragma warning restore 0618
        }
    }

    private WeatherStatus ParseJson(string json)
    {
        WeatherStatus weather = new WeatherStatus();
        try
        {
            dynamic obj = JObject.Parse(json);

            weather.weatherId = obj.weather[0].id;
            weather.main = obj.weather[0].main;
            weather.description = obj.weather[0].description;
            weather.temperature = obj.main.temp;
            weather.pressure = obj.main.pressure;
            weather.windSpeed = obj.wind.speed;
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }

        Debug.Log("City: " + city);
        Debug.Log("Wind speed: " + weather.windSpeed);
        Debug.Log("Current weather: " + (Weather)weather.TruncatedWeatherId());

        truncatedWeatherId = weather.TruncatedWeatherId();
        windSpeed = weather.windSpeed;

        SetGameWeather(weather.TruncatedWeatherId());

        return weather;
    }

    private void SetGameWeather(int weatherId)
    {
        // call update weather method from GameWeather
        weatherSystem = GameObject.Find("WeatherSystem");
        weatherSystem.GetComponent<GameWeather>().ActivateWeather(((Weather)weatherId).ToString()); //may change
    }
}