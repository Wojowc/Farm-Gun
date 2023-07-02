using System.Collections.Generic;
using UnityEngine;

public class GenerateOpponent : MonoBehaviour
{
    [SerializeField]
    private GameObject wolf, fox;

    private List<GameObject> wolfs, foxes;

    [SerializeField]
    private int amountWolf, amountFox;

    [SerializeField]
    private List<string> animalsToChaseForWolf, animalsToChaseForFox;

    [SerializeField]
    private LightingManager lightingManager;
    [SerializeField]
    private MobGenerator mobGenerator;
    [SerializeField]
    private List<Transform> possibleSpawnpoints;
    
    private void Awake()
    {
        wolfs = new ();
        foxes = new ();
        animalsToChaseForWolf = new() { "Cow", "Pig", "Sheep" };
        animalsToChaseForFox = new() { "Chicken", "Duck" };
    }

    public void InstantiateOponentsParametrized()
    {
        amountFox = (lightingManager.DaysCount + 4) / 3 + (mobGenerator.alreadySpawned[0] + mobGenerator.alreadySpawned[1]) / 5;
        amountWolf = (lightingManager.DaysCount + 5) / 6 + (mobGenerator.alreadySpawned[2] + mobGenerator.alreadySpawned[3] + mobGenerator.alreadySpawned[4])/3;
        InstantiateMultipleOpponentsForCategory(wolf, amountWolf, wolfs, animalsToChaseForWolf);
        InstantiateMultipleOpponentsForCategory(fox, amountFox, foxes, animalsToChaseForFox);
    }

    public void InstantiateMultipleOpponentsForCategory(GameObject opponent, int amount, List<GameObject> opponents, List<string> animalsToChaseForOpponent)
    {
        for (int i = 0; i < amount; i++)
        {
            Transform spawnpoint = possibleSpawnpoints[Random.Range(0,possibleSpawnpoints.Count)];
            GameObject opponentInstance = GameObject.Instantiate(opponent, spawnpoint.position, Quaternion.identity);
            opponentInstance.transform.rotation = transform.rotation;
            opponentInstance.GetComponent<Opponent>().InitAnimalsToChase(animalsToChaseForOpponent);
            opponents.Add(opponentInstance);
        }
    }

    public void DestroyAllOpponents(bool randomizeDestroyTime = false)
    {
        var randomTime = 0.0f;
        foreach (var wolf in wolfs)
        {
            if (randomizeDestroyTime)
            {
                randomTime = Random.Range(0.0f, 1.0f);
            }
            Destroy(wolf, randomTime);
        }

        wolfs.Clear();

        foreach (var fox in foxes)
        {
            if (randomizeDestroyTime)
            {
                randomTime = Random.Range(0.0f, 1.0f);
            }
            Destroy(fox, randomTime);
        }
        foxes.Clear();

    }
}