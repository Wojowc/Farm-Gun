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
    public bool IsBuff { get; set; } = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsEating = true;
        }
    }

    public void Hit(GameObject projectile)
    {
        var direction = (this.gameObject.transform.position - projectile.transform.position).normalized;
        IsHit = true;

        agent.enabled = false;
        GetComponent<Rigidbody>().AddForce(shotForce * agent.speed * direction, ForceMode.Impulse);
        agent.enabled = true;
      
    }

    private void Update()
    {
        if (IsBuff)
        {
            agent.speed += buffAmount;
            IsBuff = false;
        }

    }
}