// Conditions explained: https://openweathermap.org/weather-conditions

using System;

public class WeatherStatus
{
    public int weatherId;
    public string main;
    public string description;
    public float temperature; // in kelvin
    public float pressure;
    public float windSpeed; // in m/s

    public float Celsius()
    {
        return temperature - 273.15f;
    }

    public float Fahrenheit()
    {
        return Celsius() * 9.0f / 5.0f + 32.0f;
    }

    //returns the first N digits of a number without converting the number to the string
    public int TruncatedWeatherId()
    {
        int number = Math.Abs(weatherId);
        int N = 1; // Amount of digits required

        if (number == 0) // special case for 0 as Log of 0 would be infinity
        {
            return number;
        }

        if (number >=800 & number <=804) // special case for cloudy weather condition
        {
            return number;
        }

        // getting number of digits on this input number
        int numberOfDigits = (int)Math.Floor(Math.Log10(weatherId) + 1);

        // check if input number has more digits than the required get first N digits
        if (numberOfDigits >= N)
            return (int)Math.Truncate((number / Math.Pow(10, numberOfDigits - N)));
        else
            return number;
    }
}