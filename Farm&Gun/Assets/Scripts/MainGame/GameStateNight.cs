using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateNight : GameBaseState
{
    private GameObject lightingManagerObject;
    private LightingManager lightingManager;

    public override void EnterState(GameStateManager game)
    {
        Debug.Log($"Entered state Night");
        lightingManagerObject = game.LightingManager;
        lightingManager = lightingManagerObject.GetComponent<LightingManager>();
    }

    public override void UpdateState(GameStateManager game)
    {
        if(!lightingManager.IsNight)
        {
            game.SwitchState(game.MarketState);
        }
    }
}
