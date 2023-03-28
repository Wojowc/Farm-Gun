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
    [SerializeField]
    private float defaultZoom, longStepForward, longStepBackwards, longMaxZoom, wideStepForward, wideStepBackwards, wideMaxZoom;

    private void Awake()
    {
        defaultZoom = Camera.main.orthographicSize;
    }

    public void DisableMovement()
    {
        playerMovement.DisableMovement();
        playerAttack.DisableAttack();
    }

    public void SetDefaultZoom(float newDefaultZoom)
    {
        defaultZoom = newDefaultZoom;
    }

    private IEnumerator CameraZoomCoroutine(float stepForward, float stepBackwards, float maxZoom, float initialZoom)
    {
        while(Camera.main.orthographicSize > maxZoom)
        {
            Camera.main.orthographicSize -= stepForward * Time.deltaTime;
            yield return null;
        }
        while (Camera.main.orthographicSize < initialZoom)
        {
            Camera.main.orthographicSize += stepBackwards * Time.deltaTime;
            yield return null;
        }
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
        StartCoroutine(CameraZoomCoroutine(longStepForward, longStepBackwards, Camera.main.orthographicSize - longMaxZoom, defaultZoom));
        playerAttack.FireProjectile(playerAttack.longSweep, true, Vector2.zero);       
    }

    public void WideAttackSupport()
    {
        StartCoroutine(CameraZoomCoroutine(wideStepForward, wideStepBackwards, Camera.main.orthographicSize - wideMaxZoom, defaultZoom));
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
