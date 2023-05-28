using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateDiceroll : GameBaseState
{
    private GameObject animalGenerator;
    private MobGenerator mobGenerator;
    public override void EnterState(GameStateManager game)
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
            Time.timeScale = 1.0f;
            game.SwitchState(game.DayState);
        }
    }
}
