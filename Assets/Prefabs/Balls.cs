using UnityEngine;

public class Balls : MonoBehaviour
{
   
    public float damage;

    public UnityEngine.AI.NavMeshAgent agent;
    

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
            Attack();
            attackTimer = attackInterval;
        }
       
    }
    
    public void Attack()
    {
        
      
    }
}
