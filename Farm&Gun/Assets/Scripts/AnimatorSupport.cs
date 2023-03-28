using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSupport : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack playerAttack;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private Animator animator;

    public void DisableMovement()
    {
        playerMovement.DisableMovement();
        playerAttack.DisableAttack();
    }

    public void EndAttack()
    {
        animator.SetBool("Long Attack", false);
        animator.SetBool("Wide Attack", false);
        animator.SetBool("Shot Attack", false);
        animator.SetBool("Multishot Attack", false);
        animator.SetBool("Performing Attack", false);
        playerMovement.EnableMovement();
        playerAttack.EnableAttack();
    }

    public void LongAttackSupport()
    {
        playerAttack.FireProjectile(playerAttack.longSweep, true, Vector2.zero);
    }

    public void WideAttackSupport()
    {
        playerAttack.FireProjectile(playerAttack.wideSweep, true, Vector2.zero);
    }

    public void ShotSupport()
    {
        playerAttack.FireProjectile(playerAttack.bullet, true, new Vector2(0, 0.7f));
    }

    public void MultishotSupport()
    {
        for (int i = 0; i < playerAttack.multishot; i++)
        {
            Vector2 shift = new Vector2(Random.Range(-playerAttack.multishotSpread, playerAttack.multishotSpread), Random.Range(-playerAttack.multishotSpread, playerAttack.multishotSpread) + 0.7f);
            playerAttack.FireProjectile(playerAttack.bullet, false, shift);
        }
    }
}
