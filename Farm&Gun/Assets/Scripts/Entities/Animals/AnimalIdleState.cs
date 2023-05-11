using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class AnimalIdleState : State
{
    [SerializeField] private AnimalFollowPlayerState followPlayerState;
    [SerializeField] private AnimalRunAwayState runAwayState;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;
    [SerializeField] private float playerDistanceThreshold = 8f;
    [SerializeField] private float enemyDistanceThreshold = 5f;
    [SerializeField] private float idleAnimationDelaySec = 6f;
    [SerializeField] private float idleAnimationRadius = 2f;

    private bool _isIdleCoroutineRunning = false;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override State RunCurrentState()
    {
        if (!IsPlayerCloserThan(playerDistanceThreshold))
        {
            if (_isIdleCoroutineRunning)
            {
                StopCoroutine(IdleWalk());
                _isIdleCoroutineRunning = false;
            }
            return followPlayerState;
        }

        if (IsEnemyCloserThan(enemyDistanceThreshold))
        {
            return runAwayState;
        }

        if (!_isIdleCoroutineRunning)
        {
            StartCoroutine(IdleWalk());
        }
        return this;
    }

    private IEnumerator IdleWalk()
    {
        _isIdleCoroutineRunning = true;
        while (true)
        {
            navMesh.SetDestination(GenerateRandomLocation(idleAnimationRadius));
            yield return new WaitForSeconds(idleAnimationDelaySec);
        }
    }

    private Vector3 GenerateRandomLocation(float radius)
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

    private bool IsCloserThan(float thresholdDistance, Vector3 posA, Vector3 posB)
    {
        return Vector3.Distance(posA, posB) < thresholdDistance;
    }

    private bool IsPlayerCloserThan(float thresholdDistance)
    {
        return IsCloserThan(thresholdDistance, transform.position, player.transform.position);
    }

    private bool IsEnemyCloserThan(float thresholdDistance)
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var v in list)
        {
            if (IsCloserThan(thresholdDistance, transform.position, v.transform.position))
            {
                return true;
            }
        }
        return false;
    }
}