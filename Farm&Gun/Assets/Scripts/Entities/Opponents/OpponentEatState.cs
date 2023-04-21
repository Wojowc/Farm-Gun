using System.Collections;
using UnityEngine;

public class OpponentEatState : State
{
    [SerializeField]
    private OpponentChaseState opponentChaseState;

    [SerializeField]
    private OpponentHitState opponentHitState;

    [SerializeField]
    float coroutineTime = 5.0f;

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

        DisableMovement();
        StartCoroutine(Eat());
        return this;
    }

    private IEnumerator Eat()
    {
        yield return new WaitForSeconds(coroutineTime);
        opponent.IsBuffed = true;
        opponent.IsEating = false;
        EnableMovement();
    }

    private void DisableMovement()
    {
        opponentChaseState.Target?.GetComponent<Movement>().DisableMovement();
    }
    private void EnableMovement()
    {
        opponentChaseState.Target?.GetComponent<Movement>().EnableMovement();
    }

}

