using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private float health;
    private float maxHealth;
    public float deadDelay = 3f;

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

    protected virtual void Die()
    {
        Debug.Log("Dead1");
        Destroy(gameObject, deadDelay);
    }

    public virtual bool IsDead()
    {
        return health < 0;
    }
}
