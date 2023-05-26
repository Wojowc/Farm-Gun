using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal.Profiling;
using UnityEngine;

public class PigTowerLogic : MonoBehaviour
{
    [SerializeField] public float shootingArea = 15f;
    //[SerializeField] public float damageAmount = 5f;
    [SerializeField] public float shootingFrequency = 1f;
    //[SerializeField] public float bulletForce = 10f;
    [SerializeField] public GameObject Bullet;
    private GameObject[] enemyList;

    /////////////
    [SerializeField] GameObject fox;
    private void Start()
    {
        FindAllEnemiesWithinRange();
        StartCoroutine(ShootPoopAtOpponent());
    }

    private void Update()
    {
        FindAllEnemiesWithinRange();

        /////////////////testing
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject o = Instantiate(fox, player.transform);
            
        }
    }

    private void FindAllEnemiesWithinRange()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemyList = enemyList.Where(e => Vector3.Distance(e.transform.position, transform.position) < shootingArea).ToArray();
    }

    private IEnumerator ShootPoopAtOpponent()
    {
        while (enemyList!= null && enemyList.Length > 0)
        {
            int randomOpponent = Random.Range(0, enemyList.Length);
            Vector3 targetPos = enemyList[randomOpponent].transform.position;
            Fire(targetPos);

            yield return new WaitForSecondsRealtime(shootingFrequency);
        }
    }

    private void Fire(Vector3 targetPos)
    {
        GameObject bulletInst = Instantiate(Bullet, transform);
        bulletInst.GetComponent<Projectile>().Shoot(targetPos.normalized);
    }
}
