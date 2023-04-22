using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public GradientHealthBarPreset gradient;
    public Image fill;

    public void SetSliderValue(int value)
    {
        slider.value = value;
        fill.color = gradient.GradientPreset.Evaluate(slider.normalizedValue);
    }

    public void SetMaxValue(int maxValue)
    {
        slider.maxValue = maxValue;

        if(slider.direction == Slider.Direction.LeftToRight)
        {
            slider.value = 0;
            fill.color = gradient.GradientPreset.Evaluate(0f);
            return;
        }

        slider.value = maxValue;
        fill.color = gradient.GradientPreset.Evaluate(1f);
    }
}
