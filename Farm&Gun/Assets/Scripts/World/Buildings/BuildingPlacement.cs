using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.AI.Navigation;

public class BuildingPlacement : MonoBehaviour
{
    #region Fields

    private List<SphereCollider> sphereCollidersOfPlacedTurrets = new List<SphereCollider>();

    [SerializeField]
    private BuildingsManager buildingsManager;

    private Dictionary<BuildingType, int> buildingsAmounts = new();

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


    [SerializeField]
    public NavMeshSurface surface;

    #endregion

    #region Methods

    #region Placing turrets
    public void ChooseTurretOne()
    {
        if(!CanPlaceTower(BuildingType.Ducks))
        {
            Debug.Log("Can't place ducks tower.");
            SetBuildingModelValues();
            return;
        }
        buildingsManager.SetAmountOfTurrets(buildingsAmounts.GetValueOrDefault(BuildingType.Ducks) - 1, BuildingType.Ducks);
        buildingsAmounts = buildingsManager.GetBuildingsAmounts();
        buildingsManager.UpdateDisplayedTexts();
        buildingModel = GameObject.Instantiate(turretOne, turretsParentObject.transform);
        SetBuildingModelValues();
    }

    public void ChooseTurretTwo()
    {
        if (!CanPlaceTower(BuildingType.Pig))
        {
            Debug.Log("Can't place pig tower.");
            SetBuildingModelValues();
            return;
        }
        buildingsManager.SetAmountOfTurrets(buildingsAmounts.GetValueOrDefault(BuildingType.Pig) - 1, BuildingType.Pig);
        buildingsAmounts = buildingsManager.GetBuildingsAmounts();
        buildingsManager.UpdateDisplayedTexts();
        buildingModel = GameObject.Instantiate(turretTwo, turretsParentObject.transform);
        SetBuildingModelValues();
    }

    public void ChooseFence()
    {
        if (!CanPlaceTower(BuildingType.Fence))
        {
            Debug.Log("Can't place fence.");
            SetBuildingModelValues();
            return;
        }
        buildingsManager.SetAmountOfTurrets(buildingsAmounts.GetValueOrDefault(BuildingType.Fence) - 1, BuildingType.Fence);
        buildingsAmounts = buildingsManager.GetBuildingsAmounts();
        buildingsManager.UpdateDisplayedTexts();
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

    private bool CanPlaceTower(BuildingType towerType)
    {
        switch (towerType)
        {
            case BuildingType.Ducks:
                if(buildingsAmounts.GetValueOrDefault(BuildingType.Ducks) < 1)
                {
                    Debug.Log($"Ducks amount: {buildingsAmounts.GetValueOrDefault(BuildingType.Ducks)}");
                    return false;
                }
                return true;
            case BuildingType.Pig:
                if (buildingsAmounts.GetValueOrDefault(BuildingType.Pig) < 1)
                {
                    return false;
                }
                return true;
            case BuildingType.Fence:
                if (buildingsAmounts.GetValueOrDefault(BuildingType.Fence) < 1)
                {
                    return false;
                }
                return true;
            default: 
                return false;
        }
    }

    #endregion Placing turrets

    private bool IsOverlapping()
    {
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

            return true;
        }

        foreach (SphereCollider collider in sphereCollidersOfPlacedTurrets)
        {
            collider.enabled = true;
        }
        return false;
    }

    private void Update()
    {
        if (buildingModel == null) return;

        buildingModel.transform.position = new Vector3(playerModel.transform.position.x + 3.0f, map.transform.position.y + 1.0f, playerModel.transform.position.z);

        if(Input.GetKeyDown(KeyCode.R))
        {
            buildingModel.transform.Rotate(new Vector3(0, 90f, 0));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tried to place a turret.");

            if (IsOverlapping()) // checks overlap with other buildings
            {
                return;
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

        
            surface.BuildNavMesh();
        }

    }

    private void Start()
    {
        buildingsAmounts = buildingsManager.GetBuildingsAmounts();
    }

    #endregion Methods

}
public enum BuildingType
{
    Ducks,
    Pig,
    Fence
}
