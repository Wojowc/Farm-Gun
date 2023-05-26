using System.Collections;
using UnityEngine;

public class DucksTowerLogic : MonoBehaviour
{
    [SerializeField] public float healingArea = 15f;
    [SerializeField] public float healingAmount = 5f;
    private GameObject[] animalList;

    private void Start()
    {
        FindAllAnimals();
        StartCoroutine(HealAnimals());
    }

    private void Update()
    {
        FindAllAnimals();
    }

    private void FindAllAnimals()
    {
        animalList = GameObject.FindGameObjectsWithTag("Animal");
    }

    private IEnumerator HealAnimals()
    {
        if (animalList == null)
        {
            yield return null;
        }
        foreach (GameObject go in animalList)
        {
            if (Vector3.Distance(go.transform.position, transform.position) < healingArea)
            {
                var hm = go.GetComponent<HealthManager>();
                if (hm != null)
                    hm.Heal(5);
            }
        }

        yield return new WaitForSecondsRealtime(1);
    }
}