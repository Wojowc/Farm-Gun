using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class OpponentHitState : State
{
    [SerializeField]
    private OpponentChaseState opponentChaseState;

    [SerializeField]
    private OpponentEatState opponentEatState;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    float coroutineTime = 0.1f;

    private Opponent opponent;

    private void Awake()
    {
        opponent = gameObject.transform.parent.parent.GetComponent<Opponent>();
    }
    public override State RunCurrentState()
    {
        if (!opponent.IsEating && !opponent.IsHit)
        {
            return opponentChaseState;
        }
        else if (opponent.IsEating && !opponent.IsHit)
        {
            return opponentEatState;
        }

        opponent.IsEating = false;
        StartCoroutine(Hit());
        return this;
    }

    private IEnumerator Hit()
    {
        yield return new WaitForSeconds(coroutineTime);
        opponent.IsHit = false;
    }
}