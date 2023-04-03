using System.Collections.Generic;
using UnityEngine;

public class GenerateOpponent : MonoBehaviour
{
    [SerializeField]
    private GameObject wolf, fox;

    [SerializeField]
    private List<GameObject> wolfs, foxes;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            InstantiateOponents();
        }
    }

    void InstantiateOponents()
    {
        // jesli kilka wilkow to nalezy ich tworzyc i dodawac na liste
        GameObject wolfInstance = GameObject.Instantiate(wolf, this.transform.position, Quaternion.identity);
        wolfInstance.transform.rotation = transform.rotation;
        wolfs.Add(wolfInstance);
    }
}