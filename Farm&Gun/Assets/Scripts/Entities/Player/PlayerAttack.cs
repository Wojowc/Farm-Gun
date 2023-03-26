using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    GameObject bullet, wideSweep, longSweep;
    [SerializeField]
    float shotTime = 0.5f, multishotTime = 0.8f, wideAttackTime = 0.5f, longAttackTime = 0.5f, ammo = 20, multishot = 5, multishotSpread = 0.5f;
    [SerializeField]
    bool usingGun = true;
    bool canAttack = true;
    PlayerMovement playerMovement;


    private void Awake()
    {
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //movement guard
        if (!canAttack) return;


        //handle firing projectiles
        if (!usingGun)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                FireProjectile(longSweep, true, wideAttackTime, Vector2.zero);
            }

            else if (Input.GetButtonDown("Fire2"))
            {
                FireProjectile(wideSweep, true, longAttackTime, Vector2.zero);
            }
        }

        else if (usingGun && ammo > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ammo--;
                FireProjectile(bullet, false, shotTime, Vector2.zero);
            }

            else if (Input.GetButtonDown("Fire2"))
            {
                ammo -= multishot;
                //generate bulets randomly
                for (int i = 0; i < multishot; i++)
                {
                    Vector2 shift = new Vector2(Random.Range(-multishotSpread, multishotSpread), Random.Range(-multishotSpread, multishotSpread));
                    FireProjectile(bullet, false, multishotTime, shift);
                }
            }
        }

        //swap weapon on scroll
        if (Input.mouseScrollDelta.y > 0)
        {
            usingGun = true;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            usingGun = false;
        }
    }

    private void FireProjectile(GameObject projectile, bool melee, float attackTime, Vector2 shift)
    {
        //instantiate a projectile
        GameObject shotProjectile = GameObject.Instantiate(projectile, this.transform.position + new Vector3(shift.x,shift.y,0), Quaternion.identity);

        //give the projectile a correct rotation
        shotProjectile.transform.rotation = transform.rotation;

        //stick to body if attack is melee
        if (melee) shotProjectile.transform.parent = transform;

        //add force to the bullet
        shotProjectile.GetComponent<Projectile>().Shoot(transform.forward.normalized);

        DisableAttack(attackTime);
    }

    public void DisableAttack(float time)
    {
        Invoke("EnableAttack", time);
        canAttack = false;
    }

    public void EnableAttack()
    {
        canAttack = true;
    }

    public bool IsEnabled()
    {
        return canAttack;
    }
}
