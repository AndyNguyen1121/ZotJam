using UnityEngine;
using UnityEngine.AI;

public class GoonCombatManager : MonoBehaviour
{
    public NavMeshAgent agent;
    public float velocity;
    public Animator animator;
    public GameObject bullet;
    public Transform fireBallSpawnLocation;
    public float rotationSpeed = 10f;

    public bool canAttack = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = agent.velocity.magnitude;
        animator.SetFloat("velocity", velocity);

        HandleAttack();

       
        Vector3 dir = (PlayerManager.instance.transform.position - transform.position).normalized;
        dir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void HandleAttack()
    {
        if (canAttack && ((transform.position - 
            PlayerManager.instance.transform.position).sqrMagnitude <= (agent.stoppingDistance * agent.stoppingDistance)))
        {
            animator.CrossFade("Attack", 0.1f);
            canAttack = false;
            agent.isStopped = true;
            soundManager.instance.PlaySoundAtPosition(SoundType.ZOMBIE_GROAN, transform.position, 0.5f);
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
