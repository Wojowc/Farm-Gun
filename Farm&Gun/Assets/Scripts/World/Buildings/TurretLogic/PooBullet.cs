using UnityEngine;

public class PooBullet : MonoBehaviour
{
    private Vector3 shootDir;
    private float shootSpeed = 25f;
    private float lifetime = 5f;
    private float damage = 10f;

    private void Update()
    {
        //transform.position += (shootDir * Time.deltaTime * shootSpeed);
        transform.position = Vector3.MoveTowards(transform.position, shootDir, shootSpeed * Time.deltaTime);
    }

    public void Setup(Vector3 shootDir, float damage)
    {
        this.shootDir = shootDir;
        Destroy(gameObject, lifetime);
        this.damage = damage;
        //GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.VelocityChange) ;
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
