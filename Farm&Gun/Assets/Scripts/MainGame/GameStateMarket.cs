using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMarket : GameBaseState
{
    private GameObject marketCanvas;

    public override void EnterState(GameStateManager game)
    {
        Debug.Log($"Entered state Market");
        marketCanvas = game.MarketManager.transform.GetChild(0).gameObject;
        marketCanvas.SetActive(true);
        game.MinimapCamera.SetActive(false);
        game.Player.SetActive(false);
        game.MarketManager.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public override void UpdateState(GameStateManager game)
    {
        if (!marketCanvas.activeSelf)
        {
            game.MarketManager.SetActive(false);
            game.SwitchState(game.DicerollState);
        }
    }
    
}
