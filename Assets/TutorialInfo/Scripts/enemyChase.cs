using UnityEngine;

public class enemyChase : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent agent;
    Transform target;

    [Header("AI Tuning")]
    public float detectRadius = 100f;
    public float repathInterval = 1f;

    float repathTimer;

    void Awake() {agent = GetComponent<UnityEngine.AI.NavMeshAgent>();}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var p = GameObject.FindWithTag("Player");
        if (p) target = p.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        float sqr = (target.position - transform.position).sqrMagnitude;
        if (sqr > detectRadius * detectRadius) return;

        repathTimer -= Time.deltaTime;
        if (repathTimer <= 0f)
        {
            agent.SetDestination(target.position);
            repathTimer = repathInterval;
        }
    }
}
