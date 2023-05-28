using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameStateIntro : GameBaseState
{
    private GameObject introCanvas;
    private Button confirmButton;
    private GameStateManager stateManager;

    public override void EnterState(GameStateManager game, params string[] args)
    {
        Debug.Log($"Entered state Intro");
        game.Player.SetActive(false);
        introCanvas = game.IntroCanvas;
        confirmButton = introCanvas.GetComponentInChildren<Button>();
        stateManager = game;
        confirmButton.onClick.AddListener(ContinueAfterConfirm);
        game.MinimapCamera.SetActive(false);
    }

    public override void UpdateState(GameStateManager game)
    {
    }

    private void ContinueAfterConfirm()
    {
        introCanvas.SetActive(false);
        stateManager.SwitchState(stateManager.DicerollState);
    }
}
