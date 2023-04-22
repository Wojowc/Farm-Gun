using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private static bool buildingScreenOpen = false;

    public static bool BuildingScreenOpen {
        get {
            return buildingScreenOpen;
        }
        private set {
            buildingScreenOpen = value;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildingScreenOpen = !buildingScreenOpen;
        }
    }
}
