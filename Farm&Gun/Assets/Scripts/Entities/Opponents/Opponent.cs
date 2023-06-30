using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
    [SerializeField]
    private float shotForce;

    [SerializeField]
    private NavMeshAgent agent;

    private const float buffAmount = 2;

    [SerializeField]
    private List<string> animalsToChase;

    public bool IsEating { get; set; } = false;
    public bool IsHit { get; set; } = false;
    public bool IsBuffed { get; set; } = false;

    private void Awake()
    {
        if (IsBuffed)
        {
            agent.speed += buffAmount;
            IsBuffed = false;
        }
    }
    public void InitAnimalsToChase(List<string> animalsToChase)
    {
        this.animalsToChase = animalsToChase;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            IsEating = true;
            collision.gameObject.GetComponent<PlayerHealthManager>().DecreaseHealth(1);
        }

        foreach (var animal in animalsToChase)
        {
            if (collision.gameObject.CompareTag(animal))
            {
                IsEating = true;
                collision.gameObject.GetComponent<HealthManager>().DecreaseHealth(1);
            }
          
        }
    }

    public void Hit(GameObject projectile)
    {
        Vector3 direction = projectile.GetComponent<Rigidbody>().velocity.normalized;
        IsHit = true;

        agent.enabled = false;
        GetComponent<Rigidbody>().AddForce(shotForce * agent.speed * direction, ForceMode.Impulse);
        agent.enabled = true;

    }


    public GameObject FindTheNearestAnimalToChase()
    {
        var allAnimals = FindAllPossibleAnimalsToChase();
        if (allAnimals.Count == 0)
        {
            return GameObject.Find("Player");
        }

        return CalcualteDistancesToFindTheNearestAnimal(allAnimals);
    }

    private List<GameObject> FindAllPossibleAnimalsToChase()
    {
        var allAnimals = new List<GameObject>();
        foreach (var animal in animalsToChase)
        {
            allAnimals.AddRange(GameObject.FindGameObjectsWithTag(animal));
        }

        return allAnimals;
    }

    private GameObject CalcualteDistancesToFindTheNearestAnimal(List<GameObject> allAnimals)
    {
        GameObject animalToChase = allAnimals[0];
        float distance = Mathf.Infinity;

        foreach (var animal in allAnimals)
        {
            float newDistance = Vector3.Distance(gameObject.transform.position, animal.transform.position);
            if (newDistance < distance)
            {
                animalToChase = animal;
            }
        }

        return animalToChase;
    }
}