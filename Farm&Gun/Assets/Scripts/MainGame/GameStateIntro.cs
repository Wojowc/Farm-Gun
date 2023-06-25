using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.AI.Navigation;

public class GameStateIntro : GameBaseState
{
    private GameObject introCanvas;
    private Button confirmButton;
    private GameStateManager stateManager;

    public override void EnterState(GameStateManager game, params string[] args)
    {
        Debug.Log($"Entered state Intro");
        //game.Player.SetActive(false);
        game.Player.GetComponent<PlayerMovement>().DisableMovement();
        game.Player.GetComponentInChildren<PlayerAttack>().DisableAttack();
        game.TerrainGenerator.GetComponent<MapGenerator>().CreateMap();
        game.Surface.BuildNavMesh();
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
