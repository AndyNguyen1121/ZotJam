using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public GameObject defaultEnemy;
    public Transform player;
    public float minRadius = 20f;
    public float maxRadius = 50f;
    public float spawnInterval = 5f;
    public float rampUpTime = 60f; // Time in seconds to reach max spawn rate
    public float intervalMin = 0.5f;

    float timer, elapsed;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            var p = GameObject.FindWithTag("Player");
            if (p) player = p.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        elapsed += Time.deltaTime;
        float currentInterval = Mathf.Max(intervalMin, spawnInterval - (elapsed / rampUpTime));
        timer += Time.deltaTime;
        if (timer >= currentInterval)
        {
            timer = 0f;
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        float r = Random.Range(minRadius, maxRadius);
        float ang = Random.Range(0f, Mathf.PI * 2f);

        Vector3 pos = player.position + new Vector3(Mathf.Cos(ang), 0, Mathf.Sin(ang)) * r;
        var enemy = Instantiate(defaultEnemy, pos, Quaternion.identity);

        enemy.transform.forward = (player.position - pos).normalized;
    }
}
