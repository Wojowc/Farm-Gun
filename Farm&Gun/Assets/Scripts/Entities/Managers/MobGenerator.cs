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

    void SpawnAnimal(int animalIndexNumber)
    {
        Vector3 SpawnPoint = foundSpawnPoints[animalIndexNumber].transform.position;
        float distance = maxAnimalAmount[animalIndexNumber] % 13 / 2;
        float randomX = Random.Range(-distance, distance);
        float randomZ = Random.Range(-distance, distance);
        Vector3 randomizedSpawnPosition = new Vector3(SpawnPoint.x + randomX, SpawnPoint.y, SpawnPoint.z + randomZ);
        Instantiate(FarmMobs[animalIndexNumber], randomizedSpawnPosition, Quaternion.identity);
    }

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

    public AnimalCount GetAnimalCount()
    {
        return new AnimalCount()
        {
            ChickensAmount = alreadySpawned[0],
            DucksAmount = alreadySpawned[1],
            SheepAmount = alreadySpawned[2],
            PigsAmount = alreadySpawned[3],
            CowsAmount = alreadySpawned[4]
        };
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
            firstAnimal = firstDice[Random.Range(0, 12)];      //rolled value as animal index
            Debug.Log("Pierwsza kostka: " + firstAnimal);


            //second dice roll
            secondAnimal = secondDice[Random.Range(0, 12)];    //rolled value
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

    public class AnimalCount
    {
        public int DucksAmount;
        public int PigsAmount;
        public int ChickensAmount;
        public int SheepAmount;
        public int CowsAmount;
    }
}

