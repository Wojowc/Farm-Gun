using UnityEngine;
using UnityEngine.UI;

public class TurretBuilding : MonoBehaviour
{
    private TurretBuilding currentBuilding;

    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private KeyCode buildingKey = KeyCode.C; // C as in Construct

    private SphereCollider buildingRangeTrigger;

    [SerializeField]
    private float turretHealth;
    [SerializeField]
    private float turretCompleteness = 0f;
    [SerializeField]
    private float turretBuildingAmountNeeded = 1f;
    [SerializeField]
    private float turretBuildingSpeed = 0.1f;

    [SerializeField]
    private float buildingRange; // its best if <1f
    private bool playerIsInRange = false;

    [SerializeField]
    private GameObject loadingBar;

    private GameObject buildingBar;



    private HealthBar turretCompletenessBar; // will be created when the building is placed
    [SerializeField]
    private GradientHealthBarPreset completnessBarGradientPreset;
    [SerializeField]
    private Sprite hammerIcon;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals(playerTag))
        {
            Debug.Log("Player entered.");
            playerIsInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(playerTag))
        {
            Debug.Log("Player exited.");
            playerIsInRange = false;
        }
    }


    private void Start()
    {
        SetupHealthbar();
        currentBuilding = gameObject.GetComponent<TurretBuilding>();
        buildingRangeTrigger = gameObject.GetComponent<SphereCollider>();
        buildingRangeTrigger.radius = buildingRange;
    }

    private void Update()
    {
        if(Input.GetKeyDown(buildingKey) && playerIsInRange) 
        {
            turretCompleteness += turretBuildingSpeed;
            turretCompletenessBar.SetSliderValue(turretCompleteness);
            Debug.Log("Constructing a turret.");

        }
        if (turretCompleteness >= turretBuildingAmountNeeded)
        {
            currentBuilding.gameObject.transform.Find("BuildingRangeSphere").gameObject.SetActive(false); // removes the sphere that shows building range
            currentBuilding.enabled = false;
            buildingBar.SetActive(false);
        }
    }

    private void SetupHealthbar()
    {
        buildingBar = GameObject.Instantiate(loadingBar);
        buildingBar.gameObject.transform.parent = this.transform;
        buildingBar.transform.position = new Vector3(this.transform.position.x + 1f, this.transform.position.y + 3.5f, this.transform.position.z);
        turretCompletenessBar = buildingBar.GetComponentInChildren<HealthBar>();
        turretCompletenessBar.enabled = true;
        turretCompletenessBar.SetMaxValue(turretBuildingAmountNeeded);
        turretCompletenessBar.gradient = completnessBarGradientPreset;
        turretCompletenessBar.barIcon.sprite = hammerIcon;

        buildingBar.SetActive(true);
    }

}
