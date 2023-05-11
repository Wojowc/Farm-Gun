using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.AI;

public class AnimalFollowPlayerState : State
{
    [SerializeField] private AnimalIdleState idleState;
    [SerializeField] private AnimalRunAwayState runAwayState;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;
    [SerializeField] private float runFromOpponentRadius;
    [SerializeField] private float playerDistanceThreshold = 8f;
    [SerializeField] private float enemyDistanceThreshold = 5f;

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

        navMesh.SetDestination(player.transform.position);
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
}
