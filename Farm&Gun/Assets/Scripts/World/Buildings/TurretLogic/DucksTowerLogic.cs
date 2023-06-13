using System.Collections;
using UnityEngine;

public class DucksTowerLogic : MonoBehaviour
{
    [SerializeField] public float healingArea = 15f;
    [SerializeField] public float healingAmount = 5f;
    [SerializeField] public float healingFrequency = 1f;
    private GameObject[] animalList;
    private bool _coroutineStarted = false;

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
        animalList = GameObject.FindGameObjectsWithTag("Animal");
    }

    private IEnumerator HealAnimals()
    {
        while (true)
        {
            if (animalList != null && animalList.Length > 0)
            {
                foreach (GameObject go in animalList)
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
