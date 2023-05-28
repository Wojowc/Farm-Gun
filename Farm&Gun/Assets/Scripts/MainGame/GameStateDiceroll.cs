using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateDiceroll : GameBaseState
{
    private GameObject animalGenerator;
    private MobGenerator mobGenerator;
    public override void EnterState(GameStateManager game, params string[] args)
    {
        Debug.Log($"Entered state Diceroll");
        animalGenerator = game.AnimalGenerator;
        mobGenerator = animalGenerator.GetComponent<MobGenerator>();
    }

    public override void UpdateState(GameStateManager game)
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Pseudo throw cause its not on develop.");

            if (mobGenerator.CheckIfAllAnimalTypesPresent())
            {
                game.SwitchState(game.EndState, GameStateManager.WIN_STRING);
            }
            else
            {
                Time.timeScale = 1.0f;
                game.SwitchState(game.DayState);
            }
        }
    }
}
