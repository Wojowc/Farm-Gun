using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalRunAwayState : State
{
    [SerializeField] private AnimalFollowPlayerState followPlayerState;
    [SerializeField] private AnimalIdleState idleState;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;
    [SerializeField] private float distanceThreshold = 8f;
    [SerializeField] private float stoppingDistance = 0.5f;

    private GameObject[] enemiesList;
    private bool isRunningAway = true;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override State RunCurrentState()
    {
        enemiesList = GameObject.FindGameObjectsWithTag("Enemy");

        if (IsEnemyCloserThan(distanceThreshold))
        {
            RunAwayFromEnemies();
        }
        else
        {
            isRunningAway = false;
        }

        if (!isRunningAway)
        {
            isRunningAway = true;
            if (IsPlayerCloserThan(distanceThreshold))
                return idleState;
            else
                return followPlayerState;
        }
        return this;
    }

    private void RunAwayFromEnemies()
    {
        navMesh.stoppingDistance = stoppingDistance;
        List<GameObject> tmpEnemiesList = new List<GameObject>();
        foreach (var v in enemiesList)
        {
            if (IsCloserThan(distanceThreshold, transform.position, v.transform.position))
            {
                tmpEnemiesList.Add(v);
            }
        }

        if (tmpEnemiesList.Count > 0)
        {
            List<Vector3> vectors = new List<Vector3>();
            foreach (var v in tmpEnemiesList)
            {
                Vector3 vec = v.transform.position - transform.position;
                vec.Normalize();
                vectors.Add(vec);
            }
            Vector3 resultVec = Vector3.zero;
            foreach (var v in vectors)
            {
                resultVec += v;
            }
            resultVec.Normalize();
            resultVec += new Vector3(3f, 3f, 3f);
            navMesh.SetDestination(transform.position - resultVec);
        }
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
        foreach (var v in enemiesList)
        {
            if (IsCloserThan(thresholdDistance, transform.position, v.transform.position))
            {
                return true;
            }
        }
        return false;
    }
}