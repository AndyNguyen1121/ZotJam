using UnityEngine;
using UnityEngine.AI;

public class GoonCombatManager : MonoBehaviour
{
    public NavMeshAgent agent;
    public float velocity;
    public Animator animator;
    public GameObject bullet;
    public Transform fireBallSpawnLocation;

    public bool canAttack = true;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = agent.velocity.magnitude;
        animator.SetFloat("velocity", velocity);

        HandleAttack();
    }

    void HandleAttack()
    {
        if (canAttack && Vector3.Distance(PlayerManager.instance.transform.position, transform.position) <= agent.stoppingDistance)
        {
            animator.CrossFade("Attack", 0.1f);
            canAttack = false;
            agent.isStopped = true;
        }
    }

    void ResetAttack()
    {
        // play at start of idle animation
        canAttack = true;
        agent.isStopped = false;
    }

    public void ShootFireball()
    {
        Vector3 dir = (PlayerManager.instance.transform.position - fireBallSpawnLocation.position).normalized;
        Instantiate(bullet, fireBallSpawnLocation.position, Quaternion.LookRotation(dir));
    }
}
