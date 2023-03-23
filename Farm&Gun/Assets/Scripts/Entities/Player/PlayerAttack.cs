using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject wideSweep;
    [SerializeField]
    GameObject longSweep;
    [SerializeField]
    bool usingGun = true;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && usingGun)
        {
            FireProjectile(bullet, false);
        }

        else if(Input.GetButtonDown("Fire1") && !usingGun)
        {
            FireProjectile(longSweep, true);
        }

        else if (Input.GetButtonDown("Fire2") && !usingGun)
        {
            FireProjectile(wideSweep, true);
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            usingGun = true;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            usingGun = false;
        }
    }

    private void FireProjectile(GameObject projectile, bool melee)
    {
        //instantiate a projectile
        GameObject shotProjectile = GameObject.Instantiate(projectile, this.transform.position, Quaternion.identity);

        shotProjectile.transform.rotation = transform.rotation;

        if (melee) shotProjectile.transform.parent = transform;

        //add force to the bullet
        shotProjectile.GetComponent<Projectile>().Shoot(transform.forward.normalized);
    }
}
