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
    public GameObject[] FarmMob;//indes: [0-kura,1-kaczka,2-owca,3-swinia,4-krowa]
    [SerializeField]
    int[] alreadySpawned = { 0, 0, 0, 0, 0 };

    [SerializeField]
    private List<GameObject> FarmMobs;

    public Vector3 spawnValues; //values used to constraint? spawning region TODO
    int[] firstDice = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 3, 4 };//first dice array
    int[] secondDice = { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 3, 4 };//second dice array
    int[] maxAnimalAmount = { 30, 12, 10, 6, 3 };
    int firstAnimal, secondAnimal;


    GameObject[] foundSpawnPoints;

    void Start()
    {
        //search for spawn points
        foundSpawnPoints = GameObject.FindGameObjectsWithTag("FarmAnimalsSpawnPoint");
    }

    void SpawnAnimal(int animalIndexNumber)
    {
        Vector3 SpawnPoint = foundSpawnPoints[animalIndexNumber].transform.position; //get the correct animal spawnpoint
        float distance = maxAnimalAmount[animalIndexNumber] % 13 / 2; //create a range depending on animal type
        float randomX = UnityEngine.Random.Range(-distance, distance); //get random destination within the range for X and Z
        float randomZ = UnityEngine.Random.Range(-distance, distance);
        Vector3 randomizedSpawnPosition = new Vector3(SpawnPoint.x + randomX, SpawnPoint.y, SpawnPoint.z + randomZ); //create the random vector for spawning point
       

        GameObject farmMobInstance = GameObject.Instantiate(FarmMobs[animalIndexNumber], randomizedSpawnPosition, Quaternion.identity);
        alreadySpawned[animalIndexNumber] += 1; //increase the alreadySpawned animals value with the rolled animals
        FarmMobs.Add(farmMobInstance);
        
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

                for (int i = 0; i < animalsNewSum; i++)
                {
                    if (alreadySpawned[firstAnimal] < maxAnimalAmount[firstAnimal])
                        SpawnAnimal(firstAnimal);
                    else
                        UnityEngine.Debug.Log("Przekroczono limit tego zwierzaka :< ");
                }

                animalsNewSum = (1 + alreadySpawned[secondAnimal]) / 2;

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

