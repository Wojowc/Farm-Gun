using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    //funkcja do wyswietlania wylosowanych rzeczy

    //funkcja do odpalania losowania przy uzyciu buttona

    public void RollDice()
    {
        DiceRandomRoll dice;
        dice.StartRoll();
    }

    public void SetVolume(float volume)
    {
        //Volume = volume;
        //Debug.Log("Current volume is: " + Volume);

        //volumeValue.text = volume.ToString();
    }
}
