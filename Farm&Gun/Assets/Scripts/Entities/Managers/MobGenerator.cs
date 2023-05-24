using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobGenerator : MonoBehaviour
{
    //tablica na male i duze zwierzeta dodawanie obiektu po stworzeniu
    public GameObject[] FarmMobs;//[1-kura,2-kaczka] TODO
    [SerializeField]
    private int[] alreadySpawned = { 0, 0, 0, 0, 0 };

    public Vector3 spawnValues; //values used to constraint? spawning region TODO
    private int[] firstDice = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 3, 4 };//first dice array
    private int[] secondDice = { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 3, 4 };//second dice array
    private int[] maxAnimalAmount = { 30, 12, 10, 6, 3 };
    private int firstAnimal, secondAnimal;

    private GameObject[] foundSpawnPoints;

    public bool CheckIfAllAnimalTypesPresent()
    {
        foreach (var item in alreadySpawned)
        {
            if (item < 1)
            {
                return false;
            }
        }
        return true;
    }

    public Dictionary<AnimalType, int> GetAnimalsAmounts()
    {
        var dict = new Dictionary<AnimalType, int>();

        for (int i = 0; i < alreadySpawned.Length; i++)
        {
            dict.TryAdd((AnimalType)i, alreadySpawned[i]);
        }

        return dict;
    }

    public void UpdateAnimalAmount(AnimalType animalType, int newValue)
    {
        switch (animalType)
        {
            case AnimalType.Chicken:
                alreadySpawned[0] = newValue;
                break; 
            case AnimalType.Duck:
                alreadySpawned[1] = newValue;
                break;
            case AnimalType.Sheep:
                alreadySpawned[2] = newValue;
                break;
            case AnimalType.Pig:
                alreadySpawned[3] = newValue;
                break;
            case AnimalType.Cow:
                alreadySpawned[4] = newValue;
                break;
        }
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

    void Start()
    {
        //search for spawn points and ad correct nametags
        foundSpawnPoints = GameObject.FindGameObjectsWithTag("FarmAnimalsSpawnPoint");
    }

    void Update()
    {                                                          //game objecty w tablicy, tagi dla zwierzat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //first dice roll
            firstAnimal = firstDice[UnityEngine.Random.Range(0, 12)];      //rolled value as animal index
            Debug.Log("Pierwsza kostka: " + firstAnimal);


            //second dice roll
            secondAnimal = secondDice[UnityEngine.Random.Range(0, 12)];    //rolled value
            Debug.Log("Druga kostka: " + secondAnimal);

            if (firstAnimal == secondAnimal)                                    //if both rolled the same
            {
                                                                                //compare with animals with this index currently on farm
                int animalsNewSum = (2 + alreadySpawned[firstAnimal]) / 2;
                alreadySpawned[firstAnimal] += animalsNewSum;                   //increase the alreadySpawned animals value with the rolled animals
                
                for (int i = 0; i < animalsNewSum; i++)
                {
                    if (alreadySpawned[firstAnimal] < maxAnimalAmount[firstAnimal])
                    {
                        SpawnAnimal(firstAnimal);
                    }
                    else
                    {
                        Debug.Log("Przekroczono limit tego zwierzaka :< ");
                    }
                }
            }

            else if (firstAnimal != secondAnimal)   //if both rolled different
            {
                int animalsNewSum = (1 + alreadySpawned[firstAnimal]) / 2;
                alreadySpawned[firstAnimal] += animalsNewSum;

                for (int i = 0; i < animalsNewSum; i++)
                {
                    if (alreadySpawned[firstAnimal] < maxAnimalAmount[firstAnimal])
                    {
                        SpawnAnimal(firstAnimal);
                    }
                    else
                    {
                        Debug.Log("Przekroczono limit tego zwierzaka :< ");
                    }
                }

                animalsNewSum = (1 + alreadySpawned[secondAnimal]) / 2;
                alreadySpawned[secondAnimal] += animalsNewSum;

                for (int i = 0; i < animalsNewSum; i++)
                {
                    if (alreadySpawned[firstAnimal] < maxAnimalAmount[firstAnimal])
                    {
                        SpawnAnimal(firstAnimal);
                    }
                    else
                    {
                        Debug.Log("Przekroczono limit tego zwierzaka :< ");
                    }
                }
            }
        }
    }

    public enum AnimalType // type is also an index in the 'alreadySpawned' array
    {
        Chicken = 0,
        Duck = 1,
        Sheep = 2,
        Pig = 3,
        Cow = 4
    }
}

