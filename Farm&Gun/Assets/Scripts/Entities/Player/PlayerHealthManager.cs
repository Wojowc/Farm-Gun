using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    public override void Die()
    {
        Debug.Log("HE DED");
    }
}
