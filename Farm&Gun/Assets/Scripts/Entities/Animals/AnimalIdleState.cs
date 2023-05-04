using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalIdleState : State
{
    public override State RunCurrentState()
    {
        return this;
    }
}