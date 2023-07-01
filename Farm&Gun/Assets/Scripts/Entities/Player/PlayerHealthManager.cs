using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : HealthManager
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    Slider healthBar;

    private void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    protected override void Die()
    {
        animator.SetBool("Dead", true);
        Debug.Log("Dead2");
    }

    protected override void HealthEvent()
    {
        healthBar.value = health;
    }
}
