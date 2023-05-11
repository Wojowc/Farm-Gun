using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightBar : HealthBar
{
    public LightingManager lightingManager;
    public GradientHealthBarPreset additionalNighttimeGradient;
    public Sprite dayBarIcon;
    public Sprite nightBarIcon;

    private void Awake()
    {
        slider.direction = Slider.Direction.LeftToRight;
        slider.minValue = 0;
        slider.maxValue = lightingManager.DaylightLenght;
        slider.wholeNumbers = false;
    }

    private void Update()
    {
        if (lightingManager == null)
        {
            return;
        }
        if (lightingManager.TimeOfDay > lightingManager.DaylightLenght)
        {
            slider.maxValue = lightingManager.NightLenght;
            slider.value = lightingManager.TimeOfDay - lightingManager.DaylightLenght;
            fill.color = additionalNighttimeGradient.GradientPreset.Evaluate(slider.normalizedValue);
            barIcon.sprite = nightBarIcon;
            return;
        }

        slider.maxValue = lightingManager.DaylightLenght;
        slider.value = lightingManager.TimeOfDay;
        fill.color = gradient.GradientPreset.Evaluate(slider.normalizedValue);
        barIcon.sprite = dayBarIcon;
    }
}
