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
        if (gameObject.transform.parent.parent.GetComponent<Opponent>().GetIsEating() &&
            !gameObject.transform.parent.parent.GetComponent<Opponent>().GetIsHit())
        {
            //agent.enabled = false;
            return opponentEatState;
        }
        else if (gameObject.transform.parent.parent.GetComponent<Opponent>().GetIsHit())
        {
            return opponentHitState;
        }
        else
        {
            //Debug.Log("chasing");
           // agent.enabled = true;
            agent.SetDestination(player.transform.position);
            player.GetComponent<PlayerMovement>().EnableMovement();
            return this;
        }
    }
}
