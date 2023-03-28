using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float lifetime = 2, speed = 1000, damage = 1;
    [SerializeField]
    bool destroyOnInteract = false;

    private void OnTriggerEnter(Collider other)
    {

        //destroy self on collission
        if (destroyOnInteract && other.tag != "Projectile" && other.tag != "Player")
        {
            
            Destroy(gameObject);
            
        }
    }

    public void Shoot(Vector3 direction)
    {
        //add force to the object
        GetComponent<Rigidbody>().AddForce(direction * speed);
        Destroy(gameObject, lifetime);
    }
}
