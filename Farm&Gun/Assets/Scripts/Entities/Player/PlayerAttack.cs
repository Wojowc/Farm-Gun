using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    float bulletSpeed = 1000;
    [SerializeField]
    float bulletLifetime = 1;
    [SerializeField]
    float wideSwipeSpeed = 1000;
    [SerializeField]
    float wideSwipetime = 0.5f;
    [SerializeField]
    float longSwipeSpeed = 1000;
    [SerializeField]
    float longSwipeLifetime = 0.5f;
    bool usingGun = true;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && usingGun)
        {
            //instantiate a bullet
            GameObject shotProjectile = GameObject.Instantiate(projectile, this.transform.position, Quaternion.identity);

            //add force to the bullet
            shotProjectile.GetComponent<Projectile>().Shoot(bulletSpeed, transform.forward.normalized, bulletLifetime);
        }

        else if(Input.GetButtonDown("Fire1") && !usingGun)
        {

        }

        else if (Input.GetButtonDown("Fire2") && !usingGun)
        {

        }

        if (Input.mouseScrollDelta != new Vector2(0, 0))
        {
            Debug.Log("Swap");
        }
        
    }
}
