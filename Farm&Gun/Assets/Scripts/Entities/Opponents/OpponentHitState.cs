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
    float coroutineTime = 0.25f;

    public override State RunCurrentState()
    {
        if (!gameObject.transform.parent.parent.GetComponent<Opponent>().IsEating &&
           !gameObject.transform.parent.parent.GetComponent<Opponent>().IsHit)
        {
            return opponentChaseState;
        }
        else if (gameObject.transform.parent.parent.GetComponent<Opponent>().IsEating &&
           !gameObject.transform.parent.parent.GetComponent<Opponent>().IsHit)
        {
            return opponentEatState;
        }
        else
        {
            StartCoroutine(Hit());
            return this;
        }
    }

    private IEnumerator Hit()
    {
        yield return new WaitForSeconds(coroutineTime);
        gameObject.transform.parent.parent.GetComponent<Opponent>().IsHit = false;
    }
}