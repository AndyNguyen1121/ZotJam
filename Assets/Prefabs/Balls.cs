using UnityEngine;

public class Balls : MonoBehaviour
{
   
    public float damage;

    public UnityEngine.AI.NavMeshAgent agent;

    public float rotationSpeed;
    public float attackInterval;
    float attackTimer;
    public GameObject bullet;
    public Transform fireBallSpawnLocation;
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
        else if ((transform.position - PlayerManager.instance.transform.position).sqrMagnitude <= (agent.stoppingDistance * agent.stoppingDistance))
        {
            Attack();
            attackTimer = attackInterval;
        }
        Vector3 dir = (PlayerManager.instance.transform.position - transform.position).normalized;
        dir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
       
    }
    
    public void Attack()
    {
        Vector3 dir = (PlayerManager.instance.transform.position - fireBallSpawnLocation.position).normalized;
        Instantiate(bullet, fireBallSpawnLocation.position, Quaternion.LookRotation(dir));
        soundManager.instance.PlaySoundAtPosition(SoundType.EVIL_BALL, transform.position, 0.5f);
    }
}
