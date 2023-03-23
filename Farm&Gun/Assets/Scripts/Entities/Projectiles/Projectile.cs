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
        if (destroyOnInteract)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce(direction * speed);
        Destroy(gameObject, lifetime);
    }
}
