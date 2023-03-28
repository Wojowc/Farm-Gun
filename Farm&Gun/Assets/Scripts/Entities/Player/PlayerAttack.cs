using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    public GameObject bullet, wideSweep, longSweep, gun, fork;
    [SerializeField]
    public float ammo = 20, multishot = 5, multishotSpread = 0.5f;    
    [SerializeField]
    bool usingGun = true;
    bool canAttack = true;
    [SerializeField]
    PlayerMovement playerMovement;

    void Update()
    {

        if (animator.GetBool("Performing Attack")) return;

        BugFix();

        //movement guard
        if (!canAttack) return;

        //handle firing projectiles
        if (!usingGun)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator?.SetBool("Performing Attack", true);
                animator?.SetBool("Long Attack", true);              
            }

            else if (Input.GetButtonDown("Fire2"))
            {
                animator?.SetBool("Performing Attack", true);
                animator?.SetBool("Wide Attack", true);
            }
        }

        else if (usingGun && ammo > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ammo--;
                animator?.SetBool("Performing Attack", true);
                animator?.SetBool("Shot Attack", true);
            }

            else if (Input.GetButtonDown("Fire2"))
            {
                ammo -= multishot;
                animator?.SetBool("Performing Attack", true);
                animator?.SetBool("Multishot Attack", true);
            }
        }
        //if (Input.mouseScrollDelta.y != 0)
        //swap weapon on scroll
        if (Input.mouseScrollDelta.y != 0 || Input.GetKeyDown(KeyCode.Q))
        {
            usingGun = !usingGun;
            fork.SetActive(!fork.activeSelf);
            gun.SetActive(!gun.activeSelf);
        }
    }

    public void FireProjectile(GameObject projectile, bool melee, Vector2 shift)
    {
        //instantiate a projectile
        GameObject shotProjectile = GameObject.Instantiate(projectile, this.transform.position + new Vector3(shift.x,shift.y,0), Quaternion.identity);

        //give the projectile a correct rotation
        shotProjectile.transform.rotation = transform.rotation;

        //stick to body if attack is melee
        if (melee) shotProjectile.transform.parent = transform;

        //add force to the bullet
        shotProjectile.GetComponent<Projectile>().Shoot(transform.forward.normalized);
    }

    public void DisableAttack()
    {
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

    private void BugFix()
    {
        if (!animator.GetBool("Performing Attack"))
        {
            if (animator.GetBool("Long Attack")) animator.SetBool("Long Attack", false);
            if (animator.GetBool("Wide Attack")) animator.SetBool("Wide Attack", false);
            if (animator.GetBool("Shot Attack")) animator.SetBool("Shot Attack", false);
            if (animator.GetBool("Multishot Attack")) animator.SetBool("Multishot Attack", false);
        }
    }
}
