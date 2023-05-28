using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentHealthManager : HealthManager
{
    protected override void Die()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<StateMachineManager>().enabled = false;
        gameObject.GetComponent<Opponent>().enabled = false;
        gameObject.GetComponent<OpponentHealthManager>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        Debug.Log("Dead2");
        Destroy(gameObject, deadDelay);
    }
}


