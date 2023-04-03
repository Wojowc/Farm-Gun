using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{

    [SerializeField]
    private float health;
    [SerializeField]
    private float power;
    [SerializeField]
    private float speed;

    [SerializeField]
    private float shotForce;

    [SerializeField]
    private NavMeshAgent agent;

    private bool isEating = false;
    private bool isHit = false;
    private Vector3 direction;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isEating = true;
        }
    }

    public void Hit(GameObject projectile)
    {
        direction = (this.gameObject.transform.position - projectile.transform.position).normalized;
        isHit = true;

        agent.enabled = false;
        GetComponent<Rigidbody>().AddForce(shotForce * speed * direction, ForceMode.Impulse);
        agent.enabled = true;
    }


    public void SetIsEating(bool state)
    {
        isEating = state;
    }

    public bool GetIsEating()
    {
        return isEating;
    }

    public bool GetIsHit()
    {
        return isHit;
    }
    public void SetIsHit(bool state)
    {
        isHit = state;
    }
}