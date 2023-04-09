using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


public class AnimalMovement : MonoBehaviour
{

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject player;

    private GameObject enemy;

    private float maxDistance = 15;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {

        if (enemy = GameObject.FindGameObjectWithTag("Enemy"))
        {
            //Debug.Log("enemy found");
            FindPlaceToGo(enemy);
        }

        else
        {
            //Debug.Log("go to papa");
            //TOCHANGE animal are moving around they spawn point
            //FindPlaceToGo(gameObject.transform.parent.gameObject);

            //NOW go to Player
            agent.SetDestination(player.transform.position);
        }
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
