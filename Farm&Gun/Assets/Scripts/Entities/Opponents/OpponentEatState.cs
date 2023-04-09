using System.Collections;
using UnityEngine;

public class OpponentEatState : State
{
    [SerializeField]
    private OpponentChaseState opponentChaseState;

    [SerializeField]
    private OpponentHitState opponentHitState;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    float coroutineTime = 3.0f;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

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
            player.GetComponent<PlayerMovement>().DisableMovement();
            StartCoroutine(Eat());
            return this;
        }
    }

    private IEnumerator Eat()
    {
        yield return new WaitForSeconds(coroutineTime);
        gameObject.transform.parent.parent.GetComponent<Opponent>().IsBuffed = true;
        player.GetComponent<PlayerMovement>().EnableMovement();

    }
}

