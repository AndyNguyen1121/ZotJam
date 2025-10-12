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
        float randomScreamDelay = Random.Range(2, 10);
        InvokeRepeating("Scream", randomScreamDelay, randomScreamDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;

        }
        else if ((transform.position - PlayerManager.instance.transform.position).sqrMagnitude <= (agent.stoppingDistance *agent.stoppingDistance))
        {
            animator.SetTrigger("Attack");
            attackTimer = attackInterval;
        }
        animator.SetBool("Moving", agent.velocity.sqrMagnitude > 0);
    }
    
    public void Attack()
    {
        if ((transform.position - PlayerManager.instance.transform.position).sqrMagnitude <= (agent.stoppingDistance *agent.stoppingDistance))
        {
            PlayerManager.instance.TakeDamage(damage);
            soundManager.instance.PlaySoundAtPosition(SoundType.MONSTER_ATK, transform.position, 0.1f);
        }
      
    }

    private void Scream()
    {
        soundManager.instance.PlaySoundAtPosition(SoundType.MONSTER_SCREAM, transform.position, 0.25f);
    }
}
