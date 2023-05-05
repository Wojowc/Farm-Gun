using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class AnimalIdleState : State
{
    [SerializeField] private AnimalFollowPlayerState followPlayerState;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override State RunCurrentState()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 8f)
        {

            Debug.Log("Enter follow player state");
            return followPlayerState;
        }

        return this;
    }
}