using UnityEngine;
using UnityEngine.AI;
public class Rika : MonoBehaviour
{
    public float damage;
    public Animator animator;
    public NavMeshAgent agent;
    

    public float attackInterval;
    float attackTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;

        }
        else if ((transform.position - PlayerManager.instance.transform.position).sqrMagnitude <= agent.stoppingDistance)
        {
            animator.SetTrigger("Attack");
            attackTimer = attackInterval;
        }
        animator.SetBool("Moving", agent.velocity.sqrMagnitude > 0);
    }
    
    public void Attack()
    {
        PlayerManager.instance.TakeDamage(damage);
    }
}
