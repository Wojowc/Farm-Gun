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


    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override State RunCurrentState()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < 5f)
        {
            Debug.Log("Enter idle state");
            return idleState;
        }

        navMesh.SetDestination(player.transform.position);
        return this;
    }
}
