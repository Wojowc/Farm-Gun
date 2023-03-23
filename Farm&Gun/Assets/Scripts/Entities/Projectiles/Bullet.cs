using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 1000.0f;
    Vector3 bulletDirectiion = new Vector3(1, 0, 0);
    Rigidbody rigidbody;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(bulletDirectiion * bulletSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        Destroy(gameObject);
    }
}
