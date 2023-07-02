using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementPanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BuildingPlacementPanel;

    [SerializeField]
    private PlayerAttack PlayerModelAttackPoint; // for attack blocking while building screen is open

    public static bool BuildingScreenOpen { get; set; } = false;
    public static bool TurretSelected { get; set; } = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !TurretSelected)
        {
            BuildingScreenOpen = !BuildingScreenOpen;
            BuildingPlacementPanel.SetActive(BuildingScreenOpen);
        }

        if (BuildingPlacementPanel.activeSelf)
        {
            PlayerModelAttackPoint.DisableAttack();
            return;
        }
        PlayerModelAttackPoint.EnableAttack();
    }
}
