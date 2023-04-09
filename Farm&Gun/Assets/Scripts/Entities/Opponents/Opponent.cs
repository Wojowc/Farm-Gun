using System;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
    [SerializeField]
    private float shotForce;

    [SerializeField]
    private NavMeshAgent agent;

    private const float buffAmount = 2;


    public bool IsEating { get; set; } = false;
    public bool IsHit { get; set; } = false;
    public bool IsBuffed { get; set; } = false;

    private void Awake()
    {
        if (IsBuffed)
        {
            agent.speed += buffAmount;
            IsBuffed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsEating = true;
        }
    }

    public void Hit(GameObject projectile)
    {
        //var direction = (this.gameObject.transform.position - projectile.transform.position).normalized;
        Vector3 direction = projectile.GetComponent<Rigidbody>().velocity.normalized;
        IsHit = true;

        agent.enabled = false;
        GetComponent<Rigidbody>().AddForce(shotForce * agent.speed * direction, ForceMode.Impulse);
        agent.enabled = true;
      
    }
}