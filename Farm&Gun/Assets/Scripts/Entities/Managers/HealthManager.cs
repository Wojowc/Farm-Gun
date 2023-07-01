using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    protected float health;
    [SerializeField]
    protected float maxHealth;
    public float deadDelay = 3f;
    public bool isDead = false;

    public void DecreaseHealth(float amount)
    {
        health -= amount;
        HealthEvent();
        if (health <= 0 && !isDead) Die();
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        HealthEvent();
    }

    protected virtual void Die()
    {
        isDead = true;
        Debug.Log("Dead");
        Destroy(gameObject, deadDelay);
    }

    protected virtual void HealthEvent()
    {
        Debug.Log("Health event");
    }
}
