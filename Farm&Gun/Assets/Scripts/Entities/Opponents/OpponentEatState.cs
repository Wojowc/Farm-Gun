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

    public override State RunCurrentState()
    {
        if (!gameObject.transform.parent.parent.GetComponent<Opponent>().IsEating &&
        !gameObject.transform.parent.parent.GetComponent<Opponent>().IsHit)
        {
            return opponentChaseState;
        }
        else if (gameObject.transform.parent.parent.GetComponent<Opponent>().IsHit)
        {
            gameObject.transform.parent.parent.GetComponent<Opponent>().IsEating = false;
            return opponentHitState;
        }

        else
        {
            //TODO: target.DisableMovement()
            // player.GetComponent<PlayerMovement>().DisableMovement();
            StartCoroutine(Eat());
            return this;
        }
    }

    private IEnumerator Eat()
    {
        yield return new WaitForSeconds(coroutineTime);
        gameObject.transform.parent.parent.GetComponent<Opponent>().IsBuffed = true;
        gameObject.transform.parent.parent.GetComponent<Opponent>().IsEating = false;

        //TODO: target.EnableMovement()
        //player.GetComponent<PlayerMovement>().EnableMovement();
    }
}

