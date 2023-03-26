using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    private State currentState;

    void Update()
    {
        State nextState = currentState?.RunState();
        if (nextState != null) currentState = nextState;
    }
}
