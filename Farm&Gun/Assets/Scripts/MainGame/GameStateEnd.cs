using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateEnd : GameBaseState
{
    private GameOverManager manager;

    public override void EnterState(GameStateManager game, params string[] args)
    {
        Debug.Log($"Entered state End");

        manager = game.GameOverScreenManager.GetComponent<GameOverManager>();

        if (args.Length < 1 )
        {
            throw new System.ArgumentException("Incorrect state of end game.");
        }

        Time.timeScale = 0.0f;

        if (args[0] == GameStateManager.WIN_STRING)
        {
            manager.GameWon();
        }
        else if (args[0] == GameStateManager.LOOSE_STRING)
        {
            manager.GameLost();
        }
    }

    public override void UpdateState(GameStateManager game)
    {
    }

}
