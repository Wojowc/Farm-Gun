using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucksTowerLogic : MonoBehaviour
{
    [SerializeField] public float healingArea = 15f;
    [SerializeField] public float healingAmount = 5f;
    [SerializeField] public float healingFrequency = 1f;
    private List<GameObject> animalsList = new ();
    private bool _coroutineStarted = false;
    private string [] animals = { "Cow", "Pig", "Sheep" , "Chicken", "Duck" };


private void Update()
    {
        FindAllAnimals();
        if (!_coroutineStarted)
        {
            StartCoroutine(HealAnimals());
            _coroutineStarted = true;
        }
    }

    private void FindAllAnimals()
    {
        foreach (string animal in animals)
        {
            animalsList.AddRange(GameObject.FindGameObjectsWithTag(animal));
        }

        foreach( var a in animalsList)
        {
            Debug.Log(a);
        }
      

    }

    private IEnumerator HealAnimals()
    {
        while (true)
        {
            if (animalsList != null && animalsList.Count > 0)
            {
                foreach (GameObject go in animalsList)
                {
                    Debug.Log("heal");
                    if (Vector3.Distance(go.transform.position, transform.position) < healingArea)
                    {
                        var hm = go.GetComponent<HealthManager>();
                        if (hm != null)
                            hm.Heal(healingAmount);
                    }
                }
            }
            yield return new WaitForSecondsRealtime(healingFrequency);
        }
    }
}
