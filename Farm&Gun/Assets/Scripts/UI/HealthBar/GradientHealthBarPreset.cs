using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Healthbar Preset", menuName = "Presets/HealthbarPreset", order = 2)]
public class GradientHealthBarPreset : ScriptableObject
{
    public Gradient GradientPreset;
}
