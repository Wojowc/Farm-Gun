using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalRunAwayState : State
{
    [SerializeField] private AnimalFollowPlayerState followPlayerState;
    [SerializeField] private AnimalIdleState idleState;
    [SerializeField] private NavMeshAgent navMesh;
    private GameObject player;
    private float distanceThreshold = 8f;
    private float stoppingDistance = 2f;

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
        foreach (var e in enemiesList)
        {
            if (IsCloserThan(distanceThreshold, transform.position, e.transform.position))
            {
                tmpEnemiesList.Add(e);
            }
        }

        if (tmpEnemiesList.Count > 0)
        {
            List<Vector3> vectors = new List<Vector3>();
            foreach (var tmpEnemy in tmpEnemiesList)
            {
                Vector3 vec = tmpEnemy.transform.position - transform.position;
                vec.Normalize();
                vectors.Add(vec);
            }
            Vector3 resultVec = Vector3.zero;
            foreach (var vec in vectors)
            {
                resultVec += vec;
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
        foreach (var e in enemiesList)
        {
            if (IsCloserThan(thresholdDistance, transform.position, e.transform.position))
            {
                return true;
            }
        }
        return false;
    }
}