using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class OpponentChaseState : State
{
    [SerializeField]
    private OpponentEatState opponentEatState;

    [SerializeField]
    private OpponentHitState opponentHitState;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private float coroutineTime = 2f;

    public GameObject Target { set; get; }

    private Opponent opponent;

    private void Awake()
    {
        opponent = gameObject.transform.parent.parent.GetComponent<Opponent>();
        Target = opponent.FindTheNearestAnimalToChase();
    }
    public override State RunCurrentState()
    {
        if (opponent.IsEating && !opponent.IsHit)
        {
            return opponentEatState;
        }
        else if (opponent.IsHit)
        {
            return opponentHitState;
        }

        if (!Target)
        {
            Target = opponent.FindTheNearestAnimalToChase();
        }

        agent.SetDestination(Target.transform.position);
        StartCoroutine(FindAnimalToChase());
        return this;
    }

    private IEnumerator FindAnimalToChase()
    {
        yield return new WaitForSeconds(coroutineTime);
        Target = opponent.FindTheNearestAnimalToChase();
        agent.SetDestination(Target.transform.position);
    }
}
