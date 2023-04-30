using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack PlayerModel;

    [SerializeField]
    private GameObject Map; // y position is needed for mapping the position of the mouse on the map
    [SerializeField]
    private GameObject BuildingPlacementUICanvas;

    [SerializeField]
    private GameObject TurretsParentObject;

    [SerializeField]
    private GameObject TurretOne;
    [SerializeField]
    private GameObject TurretTwo;

    private GameObject BuildingModel; // currently selected building, will be used in update method

    public void ChooseTurretOne()
    {
        BuildingModel = GameObject.Instantiate(TurretOne, TurretsParentObject.transform);
        if (BuildingModel != null)
        {
            Debug.Log("Building Model is not null!!");
            SetDefaultBuildingValues(true, false);
            return;
        }
        PlayerModel.EnableAttack();
        SetDefaultBuildingValues(false, false);
    }

    public void ChooseTurretTwo()
    {
        BuildingModel = GameObject.Instantiate(TurretTwo, TurretsParentObject.transform);
        if (BuildingModel != null)
        {
            Debug.Log("Building Model is not null!!");
            SetDefaultBuildingValues(true, false);
            return;
        }
        PlayerModel.EnableAttack();
        SetDefaultBuildingValues(false, false);
    }

    private void SetDefaultBuildingValues(bool turretSelected, bool buildingScreenOpen)
    {
        BuildingModel.transform.position = Input.mousePosition;
        BuildingPlacementPanelManager.TurretSelected = turretSelected;
        BuildingPlacementPanelManager.BuildingScreenOpen = buildingScreenOpen;
        BuildingPlacementUICanvas.SetActive(false);
    }

    private void Update()
    {
        if (BuildingModel == null) return;

        BuildingModel.transform.position = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tried to place a turret.");
            PlayerModel.EnableAttack();
            BuildingPlacementUICanvas.SetActive(false);
            BuildingModel.gameObject.SetActive(true);
            BuildingModel = null;
        }

    }
}
