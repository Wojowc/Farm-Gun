using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateNight : GameBaseState
{
    private GameObject lightingManagerObject;
    private LightingManager lightingManager;
    private PlayerHealthManager playerHealthManager;

    public override void EnterState(GameStateManager game, params string[] args)
    {
        Debug.Log($"Entered state Night");
        lightingManagerObject = game.LightingManager;
        lightingManager = lightingManagerObject.GetComponent<LightingManager>();
    }

    public override void UpdateState(GameStateManager game)
    {
        if(playerHealthManager != null)
        {
            if (playerHealthManager.IsDead())
            {
                game.SwitchState(game.EndState, GameStateManager.LOOSE_STRING);
            }
        }

        if(!lightingManager.IsNight)
        {
            game.SwitchState(game.MarketState);
        }
    }
}