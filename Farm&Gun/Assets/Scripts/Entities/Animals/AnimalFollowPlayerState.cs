using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class AnimalFollowPlayerState : State
{
    [SerializeField] private AnimalIdleState idleState;
    [SerializeField] private AnimalRunAwayState runAwayState;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;
    [SerializeField] private float runFromOpponentRadius;
    [SerializeField] private float playerDistanceThreshold = 8f;
    [SerializeField] private float enemyDistanceThreshold = 6f;
    [SerializeField] private float playerAvgLocationRadius = 4f;

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

    private void FollowPlayer()
    {
        navMesh.SetDestination(RandomLocationAroundPlayer(playerAvgLocationRadius));
        //Vector3 vecToPlayer = transform.position - player.transform.position;
        //vecToPlayer.Normalize();
        //Vector3 normalizedPosToPlayer = transform.position - vecToPlayer;
        //navMesh.SetDestination(normalizedPosToPlayer);
    }

    private Vector3 RandomLocationAroundPlayer(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(1.1f, radius);
        randomDirection += player.transform.position;
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
