using System.Collections;
using UnityEngine;

public class DucksTowerLogic : MonoBehaviour
{
    [SerializeField] public float healingArea = 15f;
    [SerializeField] public float healingAmount = 5f;
    [SerializeField] public float healingFrequency = 1f;
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
        while (animalList != null && animalList.Length > 0)
        {
            foreach (GameObject go in animalList)
            {
                if (Vector3.Distance(go.transform.position, transform.position) < healingArea)
                {
                    var hm = go.GetComponent<HealthManager>();
                    if (hm != null)
                        hm.Heal(healingAmount);
                }
            }

            yield return new WaitForSecondsRealtime(healingFrequency);
        }
    }
}