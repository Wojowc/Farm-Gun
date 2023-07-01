using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentHealthManager : HealthManager
{
    LightingManager lightingManager;

    private void Start()
    {
        lightingManager = GameObject.Find("LightingManager").GetComponent<LightingManager>();
    }

    protected override void Die()
    {
        transform.rotation = new Quaternion(90, 0, 0, 0);
        var currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x, currentPosition.y + 1, currentPosition.z);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<StateMachineManager>().enabled = false;
        GetComponent<Opponent>().enabled = false;
        GetComponent<OpponentHealthManager>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<StateMachineManager>().enabled = false;
        transform.Find("State").gameObject.SetActive(false);
        Invoke("AfterDeath", deadDelay);
    }

    private void AfterDeath()
    {
        if (GameObject.FindObjectsOfType<Opponent>().Length <= 1)
        {
            if (lightingManager.TimeOfDay < lightingManager.DayLenght && lightingManager.TimeOfDay > lightingManager.DaylightLenght)
            {
                lightingManager.TimeOfDay = lightingManager.DayLenght;
            }
        }

        Destroy(gameObject);
    }
}


