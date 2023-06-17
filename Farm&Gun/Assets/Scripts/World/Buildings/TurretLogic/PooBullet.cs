using UnityEngine;

public class PooBullet : MonoBehaviour
{
    private Vector3 shootDir;
    private float shootSpeed = 40f;
    private float lifetime = 10f;
    private float damage = 0.5f;

    private void Update()
    {
        transform.position += shootDir * Time.deltaTime * shootSpeed;
        //transform.LookAt(Camera.main.transform.position);
    }

    public void Setup(Vector3 shootDir, float damage)
    {
        this.shootDir = shootDir;
        this.shootDir.y = (-2 * this.shootDir.y);
        Destroy(gameObject, lifetime);
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthManager>().DecreaseHealth(damage);
            Destroy(gameObject);
            Debug.Log("Auæ");
        }
    }
}
