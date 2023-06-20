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
    NavMeshAgent agent;

    [SerializeField]
    float coroutineTime = 0.25f;

    public override State RunCurrentState()
    {
        if (!gameObject.transform.parent.parent.GetComponent<Opponent>().GetIsEating() &&
           !gameObject.transform.parent.parent.GetComponent<Opponent>().GetIsHit())
        {
            return opponentChaseState;
        }
        else if (gameObject.transform.parent.parent.GetComponent<Opponent>().GetIsEating() &&
           !gameObject.transform.parent.parent.GetComponent<Opponent>().GetIsHit())
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
        gameObject.transform.parent.parent.GetComponent<Opponent>().SetIsHit(false);
    }
}