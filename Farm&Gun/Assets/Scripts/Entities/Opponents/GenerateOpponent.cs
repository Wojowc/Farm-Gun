using System.Collections.Generic;
using UnityEngine;

public class GenerateOpponent : MonoBehaviour
{
    [SerializeField]
    private GameObject wolf, fox;

    private List<GameObject> wolfs, foxes;

    [SerializeField]
    private int amountWolf, amountFox;
    
    private void Awake()
    {
        wolfs = new ();
        foxes = new ();
    }

    //TODO delete if no longer need for test 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            InstantiateOponents();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            InstantiateOponentsParametrized();
        }

    }

    //TODO delete if no longer need for test 
    void InstantiateOponents()
    {
        //WOLF
        GameObject wolfInstance = GameObject.Instantiate(wolf, this.transform.position, Quaternion.identity);
        wolfInstance.transform.rotation = transform.rotation;
        wolfs.Add(wolfInstance);

        //FOX
        //GameObject foxInstance = GameObject.Instantiate(fox, this.transform.position, Quaternion.identity);
        //foxInstance.transform.rotation = transform.rotation;
        //wolfs.Add(foxInstance);
    }

    void InstantiateOponentsParametrized()
    {
        InstantiateMultipleOpponentsForCategory(wolf, amountWolf, wolfs);
        InstantiateMultipleOpponentsForCategory(fox, amountFox, foxes);
    }

    void InstantiateMultipleOpponentsForCategory(GameObject opponent, int amount, List<GameObject> opponents)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject opponentInstance = GameObject.Instantiate(opponent, this.transform.position, Quaternion.identity);
            opponentInstance.transform.rotation = transform.rotation;
            opponents.Add(opponentInstance);
        }
    }
}