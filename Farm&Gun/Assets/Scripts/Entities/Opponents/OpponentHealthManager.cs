using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentHealthManager : HealthManager
{
    public override void Die()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //gameObject.transform.rotation = Quaternion.Euler(180, 0, 0);
       // Debug.Log(gameObject.transform.rotation);
        gameObject.GetComponent<StateMachineManager>().enabled = false;
        gameObject.GetComponent<Opponent>().enabled = false;
        gameObject.GetComponent<OpponentHealthManager>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        //gameObject.GetComponent<Rigidbody>(). = false;
       

        Destroy(gameObject, 3);

    }

}


