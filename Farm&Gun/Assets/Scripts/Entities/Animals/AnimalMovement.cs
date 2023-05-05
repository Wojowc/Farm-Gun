using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalMovement : Movement
{

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject player;

    private GameObject enemy;

    [SerializeField]
    private float maxDistance = 5;

    private bool canMove = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (canMove)
        {
            //if (enemy = GameObject.FindGameObjectWithTag("Enemy"))
            //{
            //    FindPlaceToGo(enemy);
            //}

//            else
//            {
                //TOCHANGE animal are moving around they spawn point
                //FindPlaceToGo(gameObject.transform.parent.gameObject);

                //NOW go to Player
                //agent.SetDestination(player.transform.position);
//            }
        }
    }


    //TODO Machine state
    public override void DisableMovement()
    {
        canMove = false;
        //agent.enabled = false;
    }
    public override void EnableMovement()
    {
        canMove = true;
        //agent.enabled = true;
    }

    private void FindPlaceToGo(GameObject objectToMoveAwayFrom)
    {
        NavMeshHit hit;
        Vector3 randomPoint = Vector3.zero;
        float distance = Vector3.Distance(transform.position, objectToMoveAwayFrom.transform.position);
        while (distance < maxDistance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out hit, maxDistance, NavMesh.AllAreas);
            randomPoint = hit.position;
            distance = Vector3.Distance(randomPoint, objectToMoveAwayFrom.transform.position);
        }

        // Set the destination of the NavMeshAgent to the random point on the NavMesh
        agent.SetDestination(randomPoint);
    }
}
