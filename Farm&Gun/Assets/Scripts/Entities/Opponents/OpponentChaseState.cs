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

    private GameObject target;

    private void Awake()
    {
        target = gameObject.transform.parent.parent.GetComponent<Opponent>().FindTheNearestAnimalToChase();
    }
    public override State RunCurrentState()
    {
        if (gameObject.transform.parent.parent.GetComponent<Opponent>().IsEating &&
            !gameObject.transform.parent.parent.GetComponent<Opponent>().IsHit)
        {
            return opponentEatState;
        }
        else if (gameObject.transform.parent.parent.GetComponent<Opponent>().IsHit)
        {
            return opponentHitState;
        }
        else
        {
            if (target == null)
            {
                target = gameObject.transform.parent.parent.GetComponent<Opponent>().FindTheNearestAnimalToChase();
            }
            agent.SetDestination(target.transform.position);
            StartCoroutine(FindAnimalToChase());
            return this;
        }
    }

    private IEnumerator FindAnimalToChase()
    {
        yield return new WaitForSeconds(coroutineTime);
        target = gameObject.transform.parent.parent.GetComponent<Opponent>().FindTheNearestAnimalToChase();
        agent.SetDestination(target.transform.position);
    }

}
