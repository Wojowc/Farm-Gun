using System.Collections;
using System.Linq;
using UnityEngine;

public class PigTowerLogic : MonoBehaviour
{
    [SerializeField] public float shootingArea = 15f;

    //[SerializeField] public float damageAmount = 5f;
    [SerializeField] public float shootingFrequency = 1f;

    //[SerializeField] public float bulletForce = 10f;
    [SerializeField] public GameObject Bullet;

    private GameObject[] enemyList;
    bool _coroutineStarted = false;

    private void Update()
    {
        FindAllEnemiesWithinRange();

        if (!_coroutineStarted)
        {
            StartCoroutine(ShootAtOpponent());
            _coroutineStarted = true;
        }
    }

    private void FindAllEnemiesWithinRange()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemyList = enemyList.Where(e => Vector3.Distance(e.transform.position,
            transform.position) < shootingArea).ToArray();
    }

    private IEnumerator ShootAtOpponent()
    {
        while (true)
        {
            if (enemyList != null && enemyList.Length > 0)
            {
                int randomOpponent = Random.Range(0, enemyList.Length);
                Vector3 targetPos = enemyList[randomOpponent].transform.position;
                Fire(targetPos);
            }
            yield return new WaitForSecondsRealtime(shootingFrequency);
        }
    }

    private void Fire(Vector3 targetPos)
    {
        Debug.Log("bum");
        Vector3 direction = targetPos - this.transform.position;
        Quaternion rotation = Quaternion.Euler(direction);
        GameObject bulletInst = Instantiate(Bullet, this.transform.position
            + new Vector3(0, 0, 2), rotation);
        bulletInst.GetComponent<Projectile>().Shoot(targetPos.normalized);
    }
}