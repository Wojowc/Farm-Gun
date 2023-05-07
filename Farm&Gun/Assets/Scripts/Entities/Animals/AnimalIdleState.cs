using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;
using Vector3 = UnityEngine.Vector3;

public class AnimalIdleState : State
{
    [SerializeField] private AnimalFollowPlayerState followPlayerState;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;
    private bool _isIdleCoroutineRunning = false;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override State RunCurrentState()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 8f)
        {
            Debug.Log("Enter follow player state");
            if (_isIdleCoroutineRunning)
            {
                StopCoroutine(IdleWalk());
                _isIdleCoroutineRunning = false;
            }
            return followPlayerState;
        }

        if (!_isIdleCoroutineRunning)
        {
            Debug.Log("pyk0");
            StartCoroutine(IdleWalk());
            Debug.Log("pyk1");
        }
        return this;
    }

    IEnumerator IdleWalk()
    {
        _isIdleCoroutineRunning = true;
        while (true)
        {
            navMesh.SetDestination(GenerateRandomLocation(3f));
            Debug.Log("Pyk");
            yield return new WaitForSeconds(5f);
        }
    }

    Vector3 GenerateRandomLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
            finalPosition.y = 3;
        }
        return finalPosition;
    }
}