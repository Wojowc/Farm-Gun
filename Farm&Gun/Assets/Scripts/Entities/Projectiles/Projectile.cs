using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float projectileSpeed = 3000.0f;
    Vector3 bulletDirectiion = new Vector3(1, 0, 0);
    Rigidbody rigidbody;
    float lifetime = 2.0f;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public void Shoot(float speed, Vector3 direction, float lifetime)
    {
        projectileSpeed = speed;
        bulletDirectiion = direction;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(bulletDirectiion * projectileSpeed);

        Destroy(gameObject, lifetime);
    }
}
