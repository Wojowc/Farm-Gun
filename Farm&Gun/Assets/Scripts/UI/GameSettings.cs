using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static string LocationCity = "Warsaw"; // City name variable used for in-game real time weather system
    public static float Volume = 100; // In-game volume

    [SerializeField] private TextMeshProUGUI volumeValue;

    public void SetLocationCity(string cityname)
    {
        if (!String.IsNullOrWhiteSpace(cityname))
        {
            LocationCity = cityname;
        }

        Debug.Log("Current city is set to: " + LocationCity);
    }

    public void SetVolume(float volume)
    {
        Volume = volume;
        Debug.Log("Current volume is: " + Volume);

        volumeValue.text = volume.ToString();
    }
}
