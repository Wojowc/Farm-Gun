using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingPlacement : MonoBehaviour
{
    private List<SphereCollider> sphereCollidersOfPlacedTurrets = new List<SphereCollider>();

    [SerializeField]
    private BuildingsManager buildingsManager;


    [SerializeField]
    private PlayerAttack playerModel;

    [SerializeField]
    private GameObject map; // y position is needed for mapping the position of the mouse on the map
    [SerializeField]
    private GameObject buildingPlacementUICanvas;

    [SerializeField]
    private GameObject turretsParentObject;

    [SerializeField]
    private GameObject turretOne;
    [SerializeField]
    private GameObject turretTwo;
    [SerializeField]
    private GameObject fence;

    private GameObject buildingModel; // currently selected building model
    private BoxCollider buildingBoxCollider;
    private SphereCollider buildingSphereCollider;

    public void ChooseTurretOne()
    {
        if(!CanPlaceTower(TowerType.Ducks))
        {
            SetBuildingModelValues();
        }
        buildingsManager.AmountOfTurretsOne--;
        buildingModel = GameObject.Instantiate(turretOne, turretsParentObject.transform);
        SetBuildingModelValues();
    }

    public void ChooseTurretTwo()
    {
        if (!CanPlaceTower(TowerType.Pig))
        {
            SetBuildingModelValues();
        }
        buildingsManager.AmountOfTurretsTwo--;
        buildingModel = GameObject.Instantiate(turretTwo, turretsParentObject.transform);
        SetBuildingModelValues();
    }

    public void ChooseFence()
    {
        if (!CanPlaceTower(TowerType.Fence))
        {
            SetBuildingModelValues();
        }
        buildingsManager.AmountOfFences--;
        buildingModel = GameObject.Instantiate(fence, turretsParentObject.transform);
        SetBuildingModelValues();
    }

    private void SetDefaultBuildingValues(bool turretSelected, bool buildingScreenOpen)
    {
        BuildingPlacementPanelManager.TurretSelected = turretSelected;
        BuildingPlacementPanelManager.BuildingScreenOpen = buildingScreenOpen;
        buildingPlacementUICanvas.SetActive(false);
    }

    private void SetBuildingModelValues()
    {
        if (buildingModel != null)
        {
            Debug.Log("Building Model is not null!!");
            buildingModel.layer = 7;
            buildingBoxCollider = buildingModel.GetComponent<BoxCollider>();
            buildingBoxCollider.isTrigger = true;
            buildingSphereCollider = buildingModel.GetComponent<SphereCollider>();
            SetDefaultBuildingValues(true, false);

            return;
        }
        playerModel.EnableAttack();
        SetDefaultBuildingValues(false, false);
    }

    private bool CanPlaceTower(TowerType towerType)
    {
        switch (towerType)
        {
            case TowerType.Ducks:
                if(buildingsManager.AmountOfTurretsOne < 1)
                {
                    return false;
                }
                return true;
            case TowerType.Pig:
                if (buildingsManager.AmountOfTurretsOne < 1)
                {
                    return false;
                }
                return true;
            case TowerType.Fence:
                if (buildingsManager.AmountOfFences < 1)
                {
                    return false;
                }
                return true;
            default: 
                return false;
        }
    }

    private void Update()
    {
        if (buildingModel == null) return;

        buildingModel.transform.position = new Vector3(playerModel.transform.position.x + 3.0f, map.transform.position.y + 1.0f, playerModel.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tried to place a turret.");

            foreach (SphereCollider collider in sphereCollidersOfPlacedTurrets)
            {
                collider.enabled = false;
            }

            Collider[] buildingColliders = Physics.OverlapBox(buildingModel.transform.position + buildingBoxCollider.center, buildingBoxCollider.bounds.size * 0.5f, Quaternion.identity, 128);

            if (buildingColliders.Count() > 1)
            {
                Debug.Log("Turret is overlapping with another turret.");
                Debug.Log($"This many colliders: {buildingColliders.Count()}.");
                Debug.Log("Colliders parents names:");
                foreach (Collider collider in buildingColliders)
                {
                    Debug.Log(collider.transform.parent.name);
                }

                return;
            }

            foreach (SphereCollider collider in sphereCollidersOfPlacedTurrets)
            {
                collider.enabled = true;
            }

            sphereCollidersOfPlacedTurrets.Add(buildingSphereCollider);

            playerModel.EnableAttack();
            SetDefaultBuildingValues(false, false);
            buildingBoxCollider.isTrigger = false;
            buildingBoxCollider = null;
            buildingModel.GetComponent<SphereCollider>().enabled = true;
            buildingSphereCollider = null;
            buildingModel.gameObject.SetActive(true);
            buildingModel = null;
        }

    }

    private enum TowerType
    {
        Ducks,
        Pig,
        Fence
    }
}
