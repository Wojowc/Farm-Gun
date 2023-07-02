using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameStateDay : GameBaseState
{
    private GameObject lightingManagerObject;
    private LightingManager lightingManager;
    private GameObject buildingPlacementManager;

    public override void EnterState(GameStateManager game, params string[] args)
    {
        Debug.Log($"Entered state Day");
        lightingManagerObject = game.LightingManager;
        lightingManager = lightingManagerObject.GetComponent<LightingManager>();
        buildingPlacementManager = game.BuildingPlacement.transform.GetChild(1).gameObject;
        buildingPlacementManager.SetActive(true);
        game.MinimapCamera.SetActive(true);
        game.Postprocessing.GetComponent<Volume>().profile = game.Day;
        game.Player.GetComponent<PlayerMovement>().EnableMovement();
        game.Player.GetComponentInChildren<PlayerAttack>().EnableAttack();
    }

    public override void UpdateState(GameStateManager game)
    {
        if (lightingManager.IsNight)
        {
            Debug.Log("Switching to night.");
            buildingPlacementManager.SetActive(false);
            game.SwitchState(game.NightState);
        }
    }

}
