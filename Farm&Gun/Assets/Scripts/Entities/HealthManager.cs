using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private float health;
    private float maxHealth;

    public void DecreaseHealth(float amount)
    {
        health -= amount;
        if (health <= 0) Die(); 
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
