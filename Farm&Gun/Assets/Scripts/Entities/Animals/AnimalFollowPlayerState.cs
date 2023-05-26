using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class AnimalFollowPlayerState : State
{
    [SerializeField] private AnimalIdleState idleState;
    [SerializeField] private AnimalRunAwayState runAwayState;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;
    [SerializeField] private float playerDistanceThreshold = 16f;
    [SerializeField] private float enemyDistanceThreshold = 6f;
    [SerializeField] private float playerAvgLocationRadius = 10f;
    [SerializeField] private float stoppingDistance = 3f;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override State RunCurrentState()
    {
        if (IsPlayerCloserThan(playerDistanceThreshold))
        {
            return idleState;
        }

        if (IsEnemyCloserThan(enemyDistanceThreshold))
        {
            return runAwayState;
        }

        FollowPlayer();
        return this;
    }

    private void FollowPlayer()
    {
        navMesh.stoppingDistance = stoppingDistance;
        navMesh.SetDestination(RandomLocationAroundPlayer(playerAvgLocationRadius));
    }

    private Vector3 RandomLocationAroundPlayer(float radius)
    {
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(player.transform.position, out hit, radius, NavMesh.AllAreas))
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
}