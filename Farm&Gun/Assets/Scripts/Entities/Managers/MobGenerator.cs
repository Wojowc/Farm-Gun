using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class MobGenerator : MonoBehaviour
{
    //tablica na male i duze zwierzeta dodawanie obiektu po stworzeniu
    public GameObject[] FarmMobs;//[1-kura,2-kaczka] TODO
    [SerializeField]
    int[] alreadySpawned = { 0, 0, 0, 0, 0 };

    public Vector3 spawnValues; //values used to constraint? spawning region TODO
    int[] firstDice = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 3, 4 };//first dice array
    int[] secondDice = { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 3, 4 };//second dice array
    int[] maxAnimalAmount = { 30, 12, 10, 6, 3 };
    int firstAnimal, secondAnimal;


    GameObject[] foundSpawnPoints;

    void Start()
    {
        //search for spawn points and ad correct nametags
        foundSpawnPoints = GameObject.FindGameObjectsWithTag("FarmAnimalsSpawnPoint");

    }
    void SpawnAnimal(int animalIndexNumber)
    {
        Vector3 SpawnPoint = foundSpawnPoints[animalIndexNumber].transform.position;
        float distance = maxAnimalAmount[animalIndexNumber] % 13 / 2;
        float randomX = UnityEngine.Random.Range(-distance, distance);
        float randomZ = UnityEngine.Random.Range(-distance, distance);
        Vector3 randomizedSpawnPosition = new Vector3(SpawnPoint.x + randomX, SpawnPoint.y, SpawnPoint.z + randomZ);
        Instantiate(FarmMobs[animalIndexNumber], randomizedSpawnPosition, Quaternion.identity);
    }

    void Update()
    {//game objecty w tablicy, tagi dla zwierzat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //first dice roll
            firstAnimal = firstDice[(int)UnityEngine.Random.Range(0, 12)];//rolled value as animal index
            UnityEngine.Debug.Log("Pierwsza kostka: " + firstAnimal);


            //second dice roll
            secondAnimal = secondDice[(int)UnityEngine.Random.Range(0, 12)];//rolled value
            UnityEngine.Debug.Log("Druga kostka: " + secondAnimal);

            if (firstAnimal == secondAnimal)//if both rolled the same
            {
                //compare with animals with this index currently on farm
                int animalsNewSum = (2 + alreadySpawned[firstAnimal]) / 2;
                alreadySpawned[firstAnimal] += animalsNewSum; //increase the alreadySpawned animals value with the rolled animals
                
                for (int i = 0; i < animalsNewSum; i++)
                {
                    if (alreadySpawned[firstAnimal] < maxAnimalAmount[firstAnimal])
                        SpawnAnimal(firstAnimal);
                    else
                        UnityEngine.Debug.Log("Przekroczono limit tego zwierzaka :< ");
                }
            }


            else if (firstAnimal != secondAnimal)//if both rolled different
            {
                int animalsNewSum = (1 + alreadySpawned[firstAnimal]) / 2;
                alreadySpawned[firstAnimal] += animalsNewSum;

                for (int i = 0; i < animalsNewSum; i++)
                {
                    if (alreadySpawned[firstAnimal] < maxAnimalAmount[firstAnimal])
                        SpawnAnimal(firstAnimal);
                    else
                        UnityEngine.Debug.Log("Przekroczono limit tego zwierzaka :< ");
                }

                animalsNewSum = (1 + alreadySpawned[secondAnimal]) / 2;
                alreadySpawned[secondAnimal] += animalsNewSum;

                for (int i = 0; i < animalsNewSum; i++)
                {
                    if (alreadySpawned[secondAnimal] < maxAnimalAmount[secondAnimal])
                        SpawnAnimal(secondAnimal);
                    else
                        UnityEngine.Debug.Log("Przekroczono limit tego zwierzaka :< ");
                }
            }
        }
    }
}

