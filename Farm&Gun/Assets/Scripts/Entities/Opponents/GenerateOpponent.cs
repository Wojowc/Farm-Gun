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

    
    private void Awake()
    {
        wolfs = new ();
        foxes = new ();
        animalsToChaseForWolf = new() { "Cow", "Pig", "Sheep", "Duck" };
        animalsToChaseForFox = new() { "Chicken" };
    }

    //TODO delete if no longer need for test 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            InstantiateOponentsParametrized();
        }
    }

    void InstantiateOponentsParametrized()
    {
        InstantiateMultipleOpponentsForCategory(wolf, amountWolf, wolfs, animalsToChaseForWolf);
        InstantiateMultipleOpponentsForCategory(fox, amountFox, foxes, animalsToChaseForFox);
    }

    void InstantiateMultipleOpponentsForCategory(GameObject opponent, int amount, List<GameObject> opponents, List<string> animalsToChaseForOpponent)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject opponentInstance = GameObject.Instantiate(opponent, this.transform.position, Quaternion.identity);
            opponentInstance.transform.rotation = transform.rotation;
            opponentInstance.GetComponent<Opponent>().InitAnimalsToChase(animalsToChaseForOpponent);
            opponents.Add(opponentInstance);
        }
    }
}