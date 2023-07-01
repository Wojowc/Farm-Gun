using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHealthManager : HealthManager
{
    MobGenerator animalManager;

    private void Start()
    {
        animalManager = GameObject.Find("AnimalGenerator").GetComponent<MobGenerator>();
    }

    protected override void Die()
    {
        isDead = true;
        int i = System.Array.IndexOf(animalManager.animalLables, gameObject.tag.ToString());
        animalManager.alreadySpawned[i] -= 1;
        Destroy(gameObject, deadDelay);
        enabled = false;
    }
}
