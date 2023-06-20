using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateNight : GameBaseState
{
    private GameObject lightingManagerObject;
    private LightingManager lightingManager;
    private PlayerHealthManager playerHealthManager;
    private GenerateOpponent oponentGenerator;

    public override void EnterState(GameStateManager game, params string[] args)
    {
        Debug.Log($"Entered state Night");
        lightingManagerObject = game.LightingManager;
        lightingManager = lightingManagerObject.GetComponent<LightingManager>();
        oponentGenerator = game.OpponentsGenerator.GetComponent<GenerateOpponent>();
        oponentGenerator.InstantiateOponentsParametrized(); // to change when the final game comes out
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
            oponentGenerator.DestroyAllOpponents();
            game.SwitchState(game.MarketState);
        }
    }
}
