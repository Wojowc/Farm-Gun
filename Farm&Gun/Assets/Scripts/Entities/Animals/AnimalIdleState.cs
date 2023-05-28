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
    [SerializeField] private float playerDistanceThreshold = 20f;
    [SerializeField] private float enemyDistanceThreshold = 6f;
    [SerializeField] private float idleAnimationDelaySec = 15f;
    [SerializeField] private float idleAnimationRadius = 3f;
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

        IdleStateAnim();
        return this;
    }

    private void IdleStateAnim()
    {
        navMesh.stoppingDistance = stoppingDistance;
        if (!_isIdleCoroutineRunning)
        {
            StartCoroutine(IdleWalk());
        }
    }

    private IEnumerator IdleWalk()
    {
        _isIdleCoroutineRunning = true;
        while (true)
        {
            navMesh.SetDestination(GenerateRandomLocation(idleAnimationRadius));
            yield return new WaitForSeconds(Random.Range(idleAnimationDelaySec - idleAnimationDelaySec / 2,
                idleAnimationDelaySec + idleAnimationDelaySec / 2));
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var e in enemies)
        {
            if (IsCloserThan(thresholdDistance, transform.position, e.transform.position))
            {
                return true;
            }
        }
        return false;
    }
}