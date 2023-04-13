using UnityEngine;
using UnityEngine.AI;

public class OpponentChaseState : State
{
    [SerializeField]
    private OpponentEatState opponentEatState;

    [SerializeField]
    private OpponentHitState opponentHitState;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private NavMeshAgent agent;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            agent.SetDestination(player.transform.position);
            return this;
        }
    }
}
