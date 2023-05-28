using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateEnd : GameBaseState
{
    public override void EnterState(GameStateManager game)
    {
        Debug.Log($"Entered state End");
    }

    public override void UpdateState(GameStateManager game)
    {
    }

}
