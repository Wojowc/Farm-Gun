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
    private PlayerCamera playerCamera;
    [SerializeField]
    private float longStepForward, longStepBackwards, longMaxZoom, wideStepForward, wideStepBackwards, wideMaxZoom;

    public void Start()
    {
        playerCamera = GameObject.FindObjectOfType<PlayerCamera>();
    }

    public void DisableMovement()
    {
        playerMovement.DisableMovement();
        playerAttack.DisableAttack();
    }

    //called at the end of attack
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

    //animates and runs long attack 
    public void LongAttackSupport()
    {
        StartCoroutine(playerCamera.ZoomCoroutine(longStepForward, longStepBackwards, Camera.main.orthographicSize - longMaxZoom));
        playerAttack.FireProjectile(playerAttack.longSweep, true, Vector2.zero);       
    }

    //animates and runs wide attack 
    public void WideAttackSupport()
    {
        StartCoroutine(playerCamera.ZoomCoroutine(wideStepForward, wideStepBackwards, Camera.main.orthographicSize - wideMaxZoom));
        playerAttack.FireProjectile(playerAttack.wideSweep, true, Vector2.zero);
    }

    //runs shot attack
    public void ShotSupport()
    {
        playerAttack.FireProjectile(playerAttack.bullet, true, new Vector2(0, 0.7f));
    }

    //runs multipleShot attack
    public void MultishotSupport()
    {
        for (int i = 0; i < playerAttack.multishot; i++)
        {
            Vector2 shift = new Vector2(Random.Range(-playerAttack.multishotSpread, playerAttack.multishotSpread), Random.Range(-playerAttack.multishotSpread, playerAttack.multishotSpread) + 0.7f);
            playerAttack.FireProjectile(playerAttack.bullet, false, shift);
        }
    }
}
