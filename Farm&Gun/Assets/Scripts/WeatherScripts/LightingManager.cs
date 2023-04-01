using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightingManager : MonoBehaviour
{
    [SerializeField]
    private Light DirectionalLight;
    [SerializeField]
    private LightingPreset Preset;

    [SerializeField]
    public int DaylightLenght = 8;
    [SerializeField]
    public int NightLenght = 16;

    private int DayLenght;

    private float TimeOfDay;

    private void Awake()
    {
        DayLenght = DaylightLenght + NightLenght;

        SetStartLightingValues(Preset.DirectionalColor);
        SetStartLightingValues(Preset.AmbientColor);
        SetStartLightingValues(Preset.FogColor);
    }

    private void Update()
    {
        if (Preset == null)
        {
            return;
        }

        if(DayLenght <= 0)
        {
            return;
        }

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= DayLenght;
            Debug.Log($"Current time is: {TimeOfDay}");
        }

        float currentLightingSpeed;

        if(TimeOfDay < DaylightLenght)
        {
            currentLightingSpeed = DayLenght * ((float)DaylightLenght / (float)NightLenght);
        }
        else
        {
            currentLightingSpeed = DayLenght * ((float)NightLenght / (float)DaylightLenght);
        }

        UpdateLighting(TimeOfDay / currentLightingSpeed);
        // TimeOfDay = 8 / 8 = 1
        // TimeOfDay = 9 / 40 = 1
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -170, 0));
        }
    }

    private void SetStartLightingValues(Gradient gradientToSet)
    {
        var dayAndNightColors = new GradientColorKey[2];
        var dayAndNightColorsAlphas = new GradientAlphaKey[2];

        dayAndNightColors[0].color = gradientToSet.colorKeys[0].color;
        dayAndNightColors[1].color = gradientToSet.colorKeys[gradientToSet.colorKeys.Length - 1].color;

        dayAndNightColors[0].time = (float)DaylightLenght / (float)DayLenght;
        dayAndNightColors[1].time = (float)NightLenght / (float)DayLenght;

        dayAndNightColorsAlphas[0].alpha = 1f;
        dayAndNightColorsAlphas[0].time = 0f;
        dayAndNightColorsAlphas[1].alpha = 1f;
        dayAndNightColorsAlphas[1].time = 1f;

        gradientToSet.SetKeys(dayAndNightColors, dayAndNightColorsAlphas);
    }

    // sets a directional light to use if its not set
    private void OnValidate()
    {
        DayLenght = DaylightLenght + NightLenght;
        SetStartLightingValues(Preset.DirectionalColor);
        SetStartLightingValues(Preset.AmbientColor);
        SetStartLightingValues(Preset.FogColor);

        if (DayLenght < 0)
        {
            return;
        }

        if (DirectionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
            return;
        }

        Light[] lights = GameObject.FindObjectsOfType<Light>();

        foreach (Light light in lights)
        {
            if(light.type == LightType.Directional)
            {
                DirectionalLight = light;
                return;
            }
        }
    }
}
