using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amountOfTurretsPigsText;
    [SerializeField]
    private TextMeshProUGUI amountOfTurretsDucksText;
    [SerializeField]
    private TextMeshProUGUI amountOfFencesText;

    [SerializeField]
    private int[] amountOfBuildings = { 0, 0, 0 };

    public Dictionary<BuildingType, int> GetBuildingsAmounts()
    {
        var dict = new Dictionary<BuildingType, int>();

        for (int i = 0; i < amountOfBuildings.Length; i++)
        {
            dict.TryAdd((BuildingType)i, amountOfBuildings[i]);
        }

        return dict;
    }

    public void SetAmountOfTurrets(int amountOfTurrets, BuildingType towerType)
    {
        switch (towerType)
        {
            case BuildingType.Ducks:
                amountOfBuildings[0] = amountOfTurrets;
                break;
            case BuildingType.Pig:
                amountOfBuildings[1] = amountOfTurrets;
                break;
            case BuildingType.Fence:
                amountOfBuildings[2] = amountOfTurrets;
                break;
        }
    }

    public void UpdateDisplayedTexts()
    {
        amountOfTurretsDucksText.text = "Amount: " + amountOfBuildings[(int)BuildingType.Ducks].ToString();
        amountOfTurretsPigsText.text = "Amount: " + amountOfBuildings[(int)BuildingType.Pig].ToString();
        amountOfFencesText.text = "Amount: " + amountOfBuildings[(int)BuildingType.Fence].ToString();
    }

    private void Awake()
    {
        SetAmountOfTurrets(amountOfBuildings[(int)BuildingType.Ducks], BuildingType.Ducks);
        SetAmountOfTurrets(amountOfBuildings[(int)BuildingType.Pig], BuildingType.Pig);
        SetAmountOfTurrets(amountOfBuildings[(int)BuildingType.Fence], BuildingType.Fence);
        UpdateDisplayedTexts();
    }

}
