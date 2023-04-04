using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager
{
    [SerializeField]
    Animator animator;
    protected override void Die()
    {
        animator.SetBool("Dead", true);
        Debug.Log("Dead2");
    }
}
