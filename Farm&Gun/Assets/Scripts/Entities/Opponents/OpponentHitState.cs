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
        yield return new WaitForSeconds(1.0f);
        gameObject.transform.parent.parent.GetComponent<Opponent>().SetIsHit(false);
    }
}