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
        if (!gameObject.transform.parent.parent.GetComponent<Opponent>().GetIsEating() &&
        !gameObject.transform.parent.parent.GetComponent<Opponent>().GetIsHit())
        {
            return opponentChaseState;
        }
        else if (gameObject.transform.parent.parent.GetComponent<Opponent>().GetIsHit())
        {
            return opponentHitState;
        }

        else
        {
            //Debug.Log("eating");
            player.GetComponent<PlayerMovement>().DisableMovement();
            StartCoroutine(Eat());
            return this;
        }
    }

    private IEnumerator Eat()
    {
        yield return new WaitForSeconds(coroutineTime);
        gameObject.transform.parent.parent.GetComponent<Opponent>().SetIsEating(false);
        player.GetComponent<PlayerMovement>().EnableMovement();
    }
}
