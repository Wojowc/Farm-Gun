using UnityEngine;

public abstract class GameBaseState : MonoBehaviour
{
    public abstract void EnterState(GameStateManager game);
    public abstract void UpdateState(GameStateManager game);
}
