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
    [SerializeField] private float playerDistanceThreshold = 12f;
    [SerializeField] private float enemyDistanceThreshold = 6f;
    [SerializeField] private float idleAnimationDelaySec = 12f;
    [SerializeField] private float idleAnimationRadius = 2f;
    [SerializeField] private float stoppingDistance = 0.5f;

    private bool _isIdleCoroutineRunning = false;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override State RunCurrentState()
    {
        if (!IsPlayerCloserThan(playerDistanceThreshold))
        {
            navMesh.ResetPath();
            if (_isIdleCoroutineRunning)
            {
                StopCoroutine(IdleWalk());
            }
            return followPlayerState;
        }

        if (IsEnemyCloserThan(enemyDistanceThreshold))
        {
            navMesh.ResetPath();
            return runAwayState;
        }

        IdleState();
            return this;
    }

    private IEnumerator IdleWalk()
    {
        _isIdleCoroutineRunning = true;
        while (true)
        {
            navMesh.SetDestination(GenerateRandomLocation(idleAnimationRadius));
            yield return new WaitForSeconds(idleAnimationDelaySec);
            navMesh.ResetPath();
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
    private void IdleState()
    {
        navMesh.stoppingDistance = stoppingDistance;
        if (!_isIdleCoroutineRunning)
        {
            StartCoroutine(IdleWalk());
        }
    }
}