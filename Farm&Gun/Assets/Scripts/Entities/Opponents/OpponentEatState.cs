using System.Collections;
using UnityEngine;

public class OpponentEatState : State
{
    [SerializeField]
    private float cooldownTime = 1.5f;

    private float nextAttackTime = 0;

    [SerializeField]
    private OpponentChaseState opponentChaseState;

    [SerializeField]
    private OpponentHitState opponentHitState;

    [SerializeField]
    float coroutineTime = 2.0f;

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

        if (opponent.IsHit)
        {
            opponent.IsEating = false;
            return opponentHitState;
        }

        if(Time.time > nextAttackTime && opponent.target != null)
        {
            nextAttackTime = Time.time + cooldownTime;
            opponent.target.GetComponent<HealthManager>().DecreaseHealth(opponent.damage);
            opponent.target = null;
        }


        opponent.IsEating = false;
        return this;

        //DisableMovement();
        //StartCoroutine(Eat());
        //return this;
    }

    private IEnumerator Eat()
    {
        yield return new WaitForSeconds(coroutineTime);
        //opponent.IsBuffed = true;
        opponent.IsEating = false;
        EnableMovement();
    }

    private void DisableMovement()
    {
        if (opponentChaseState.Target != null)
            opponentChaseState.Target.GetComponent<Movement>().DisableMovement();
    }
    private void EnableMovement()
    {
        if (opponentChaseState.Target != null)
            opponentChaseState.Target.GetComponent<Movement>().EnableMovement();
    }

}

