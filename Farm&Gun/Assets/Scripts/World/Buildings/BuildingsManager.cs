using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TowerType = BuildingPlacement.TowerType;

public class BuildingsManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amountOfTurretsOneText;
    [SerializeField]
    private TextMeshProUGUI amountOfTurretsTwoText;
    [SerializeField]
    private TextMeshProUGUI amountOfFencesText;


    public int AmountOfTurretsOne;
    public int AmountOfTurretsTwo;
    public int AmountOfFences;

    public void SetAmountOfTurrets(int amountOfTurrets, TowerType towerType)
    {
        switch (towerType)
        {
            case TowerType.Ducks:
                AmountOfTurretsOne = amountOfTurrets;
                amountOfTurretsOneText.text = "Amount: " + amountOfTurrets.ToString();
                break;
            case TowerType.Pig:
                AmountOfTurretsTwo = amountOfTurrets;
                amountOfTurretsTwoText.text = "Amount: " + amountOfTurrets.ToString();
                break;
            case TowerType.Fence:
                AmountOfFences = amountOfTurrets;
                amountOfFencesText.text = "Amount: " + amountOfTurrets.ToString();
                break;
        }
    }

    private void Awake()
    {
        SetAmountOfTurrets(AmountOfTurretsOne, TowerType.Ducks);
        SetAmountOfTurrets(AmountOfTurretsTwo, TowerType.Pig);
        SetAmountOfTurrets(AmountOfFences, TowerType.Fence);
    }

}
