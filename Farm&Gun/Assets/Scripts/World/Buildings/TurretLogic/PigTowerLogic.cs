using System.Collections;
using System.Linq;
using UnityEngine;

public class PigTowerLogic : MonoBehaviour
{
    public float shootingArea = 15f;
    public float shootingFrequency = 1f;
    public float damage = 2f;
    [SerializeField] public GameObject Bullet;

    private GameObject[] enemyList;
    private bool _coroutineStarted = false;

    private void Update()
    {
        if (GetComponent<TurretBuilding>().GetTurretCompleteness() < 1) return;
        
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
                GameObject bullet = Instantiate(
                    Bullet, transform.position + new Vector3(0, 5, 0),
                    Quaternion.LookRotation(targetPos.normalized, Vector3.up));
                Destroy(bullet, 3);
                var bul = bullet.GetComponent<PooBullet>();
                if (bul != null)
                    bul.Setup(/*(targetPos - transform.position).normalized*/targetPos,
                        damage);
            }
            yield return new WaitForSecondsRealtime(shootingFrequency);
        }
    }
}