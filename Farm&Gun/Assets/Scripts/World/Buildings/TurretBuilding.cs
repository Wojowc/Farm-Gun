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

    // healthbar preset
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
        buildingRangeTrigger.enabled = true;
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
            currentBuilding.enabled = false;
            buildingBar.SetActive(false);
        }
    }

    private void SetupHealthbar()
    {
        buildingBar = GameObject.Instantiate(loadingBar, this.transform);
        buildingBar.transform.Rotate(new Vector3(90f, 0, 0));
        buildingBar.transform.position = new Vector3(buildingBar.transform.position.x, buildingBar.transform.position.y + 3.5f, buildingBar.transform.position.z);
        buildingBar.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        turretCompletenessBar = buildingBar.GetComponentInChildren<HealthBar>();
        turretCompletenessBar.enabled = true;
        turretCompletenessBar.SetMaxValue(turretBuildingAmountNeeded);
        turretCompletenessBar.gradient = completnessBarGradientPreset;
        turretCompletenessBar.barIcon.sprite = hammerIcon;

        buildingBar.SetActive(true);
    }

}
