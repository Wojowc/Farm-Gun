using Newtonsoft.Json.Linq;
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

    private int _previousWeather = 0;

    private void Awake()
    {
        StartCoroutine(GetRealWeather());
    }

    private IEnumerator GetRealWeather()
    {
        city = GameSettings.LocationCity;

        while (true)
        {
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
            yield return new WaitForSeconds(180f);
        }
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
                StartCoroutine(ParseJson(webRequest.downloadHandler.text));
            }
#pragma warning restore 0618
        }
    }

    private IEnumerator ParseJson(string json)
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

        yield return new WaitForSeconds(2f);
        if (_previousWeather != weather.TruncatedWeatherId())
        {
            SetGameWeather(weather.TruncatedWeatherId(), weather.windSpeed);
        }
    }

    private void SetGameWeather(int weatherId, float windSpeed)
    {
        _previousWeather = weatherId;
        GameObject weatherSystem = GameObject.Find("WeatherSystem");
        weatherSystem.GetComponent<GameWeather>().weatherState = (Weather)weatherId;
        weatherSystem.GetComponent<GameWeather>().windSpeed = windSpeed;
        weatherSystem.GetComponent<GameWeather>().RestartWeather();
    }
}