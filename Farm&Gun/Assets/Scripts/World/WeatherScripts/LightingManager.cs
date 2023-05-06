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

    public int DaylightLenght = 8;
    public int NightLenght = 16;

    private int DayLenght { get; set; }

    public float TimeOfDay { get; set; }


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

        if (DayLenght <= 0)
        {
            return;
        }

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= DayLenght;
            //Debug.Log($"Current time is: {TimeOfDay}");
        }

        UpdateLighting();
    }

    private void UpdateLighting()
    {
        if (DirectionalLight == null)
        {
            Debug.LogError("Directional light is not set!");
            return;
        }

        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(TimeOfDay / DayLenght);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(TimeOfDay / DayLenght);

        DirectionalLight.color = Preset.DirectionalColor.Evaluate(TimeOfDay / DayLenght);
        if(TimeOfDay < DaylightLenght)
        {
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3(TimeOfDay/DaylightLenght * 180f, -170, 0));
        }
        else
        {
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3(180f + (TimeOfDay - DaylightLenght)/NightLenght * 180f, -170, 0));
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
