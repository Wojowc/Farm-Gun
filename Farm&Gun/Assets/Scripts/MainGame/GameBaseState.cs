using UnityEngine;

public abstract class GameBaseState : MonoBehaviour
{
    public abstract void EnterState(GameStateManager game, params string[] args);
    public abstract void UpdateState(GameStateManager game);
}
