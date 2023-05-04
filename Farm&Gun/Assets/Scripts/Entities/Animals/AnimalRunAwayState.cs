using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRunAwayState : State
{
    public override State RunCurrentState()
    {
        return this;
    }
}
