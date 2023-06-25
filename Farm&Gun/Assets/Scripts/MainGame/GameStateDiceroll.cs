using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateDiceroll : GameBaseState
{
    private GameObject animalGenerator;
    private MobGenerator mobGenerator;
    private bool isDone = false;

    public override void EnterState(GameStateManager game, params string[] args)
    {
        Debug.Log($"Entered state Diceroll");
        animalGenerator = game.AnimalGenerator;
        mobGenerator = animalGenerator.GetComponent<MobGenerator>();
        game.DiceRollComponents.SetActive(true);
        game.LightingManager.SetActive(false);
        game.DayNightBarCanvas.SetActive(false);
        game.DiceRollComponents.GetComponentInChildren<DiceRandomRoll>().OkButton.onClick.AddListener(ContinueAfterConfirm);
        Time.timeScale = 1.0f;
    }

    public override void UpdateState(GameStateManager game)
    {
        if(isDone)
        {
            game.DiceRollComponents.SetActive(false);
            if (mobGenerator.CheckIfAllAnimalTypesPresent())
            {
                game.SwitchState(game.EndState, GameStateManager.WIN_STRING);
            }
            else
            {
                game.LightingManager.SetActive(true);
                game.DayNightBarCanvas.SetActive(true);
                game.SwitchState(game.DayState);
            }
            isDone = false;
        }
    }

    private void ContinueAfterConfirm()
    {
        isDone = true;
    }
}
