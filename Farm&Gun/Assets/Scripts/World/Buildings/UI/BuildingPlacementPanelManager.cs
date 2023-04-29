using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementPanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BuildingPlacementUICanvas;

    [SerializeField]
    private PlayerAttack PlayerModelAttackPoint; // for attack blocking while building screen is open

    public static bool BuildingScreenOpen { get; set; } = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BuildingScreenOpen = !BuildingScreenOpen;
            BuildingPlacementUICanvas.SetActive(BuildingScreenOpen);
        }

        if (BuildingPlacementUICanvas.activeSelf)
        {
            PlayerModelAttackPoint.DisableAttack();
            return;
        }
        PlayerModelAttackPoint.EnableAttack();
    }
}
