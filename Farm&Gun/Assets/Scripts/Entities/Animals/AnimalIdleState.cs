using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;

public class AnimalIdleState : State
{
    [SerializeField] private AnimalFollowPlayerState followPlayerState;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;
    private bool isIdleCoroutineRunning = false;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override State RunCurrentState()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 8f)
        {
            Debug.Log("Enter follow player state");
            if (isIdleCoroutineRunning)
            {
                StopCoroutine(IdleWalk());
                isIdleCoroutineRunning = false;
            }
            return followPlayerState;
        }

        if (!isIdleCoroutineRunning)
        {
            Debug.Log("pyk0");
            StartCoroutine(IdleWalk());
            Debug.Log("pyk1");
        }
        return this;
    }

    IEnumerator IdleWalk()
    {
        isIdleCoroutineRunning = true;
        Vector3 randomDirection = Random.insideUnitSphere * 3;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 3, NavMesh.AllAreas);
        navMesh.SetDestination(hit.position);
        Debug.Log("Pyk");
        yield return new WaitForSeconds(2f);
    }
}