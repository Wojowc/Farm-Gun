using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 10.0f;
    Vector3 bulletDirectiion = new Vector3(1, 0, 0);
    Rigidbody rigidbody;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(bulletDirectiion * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += bulletDirectiion * bulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        Destroy(transform.parent.gameObject);
    }
}
