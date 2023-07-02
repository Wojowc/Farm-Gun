using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameStateNight : GameBaseState
{
    private GameObject lightingManagerObject;
    private LightingManager lightingManager;
    private PlayerHealthManager playerHealthManager;
    private GenerateOpponent oponentGenerator;

    public override void EnterState(GameStateManager game, params string[] args)
    {
        Debug.Log($"Entered state Night");
        game.Player.GetComponent<PlayerMovement>().EnableMovement();
        game.Player.GetComponentInChildren<PlayerAttack>().EnableAttack();
        lightingManagerObject = game.LightingManager;
        lightingManager = lightingManagerObject.GetComponent<LightingManager>();
        oponentGenerator = game.OpponentsGenerator.GetComponent<GenerateOpponent>();
        oponentGenerator.InstantiateOponentsParametrized(); // to change when the final game comes out
        game.Postprocessing.GetComponent<Volume>().profile = game.Night;
    }

    public override void UpdateState(GameStateManager game)
    {
        if(playerHealthManager != null)
        {
            if (playerHealthManager.isDead)
            {
                game.SwitchState(game.EndState, GameStateManager.LOOSE_STRING);
            }
        }

        if(!lightingManager.IsNight)
        {
            oponentGenerator.DestroyAllOpponents();

            bool winCondition = true;
            foreach (int a in game.AnimalGenerator.GetComponent<MobGenerator>().alreadySpawned)
            {
                if (a < 1) winCondition = false;
            }

            if (winCondition)
            {
                game.currentState = game.EndState;
                game.SwitchState(game.EndState, GameStateManager.WIN_STRING);
            }

            else game.SwitchState(game.MarketState);
        }
    }
}
