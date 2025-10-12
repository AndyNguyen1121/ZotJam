using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public static enemySpawner Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public GameObject defaultEnemy;
    public Transform player;
    public float minRadius = 20f;
    public float maxRadius = 50f;
    public float spawnInterval = 5f;
    public float rampUpTime = 60f; // Time in seconds to reach max spawn rate
    public float intervalMin = 0.5f;

    public int waveCounter = 1;
    public int enemiesToSpawn = 0;
    public bool isWaveClear = true;
    public bool startWave = true;
    public bool isSecondStage = false;

    float timer, secondStageSpawnTimer;

    

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

        // wave based spawner

        if (isWaveClear)
        {
            isWaveClear = false;
            if (startWave)
            {
                enemiesToSpawn = Mathf.CeilToInt(Mathf.Exp((waveCounter / 10f) - 1) + 10); //calculates number of enemies to spawn this wave, exponential growth
                startWave = false;
                Debug.LogError("Enemies to spawn: " + enemiesToSpawn);
            }


            // 1st stage of wave
            for (int i = 0; i < 5; i++)
            {
                SpawnOne();
                enemiesToSpawn--;
                Debug.LogError("1st stage i: " + i);
            }

            if (enemiesToSpawn > 0)
            {
                isSecondStage = true;
                secondStageSpawnTimer = 0f;
            }

            // 2nd stage of wave

            if (enemiesToSpawn == 0)
            {

                //startWave = true;
            }
        }

        if (isSecondStage && enemiesToSpawn >= 0)
            {
                secondStageSpawnTimer += Time.deltaTime;
                float timeBetweenSpawn = Random.Range(3f, 11f); // min is inclusive, max is exclusive

                if (secondStageSpawnTimer > timeBetweenSpawn)
                {
                    secondStageSpawnTimer = 0f;
                    int amountToSpawn = Random.Range(3, 11);
                    for (int j = 0; j < amountToSpawn; j++) // spawn a group of enemies
                    {
                        SpawnOne();
                        enemiesToSpawn--;
                        Debug.LogError("2nd stage amountToSpawn j: " + j);
                    }

                }

            }

        // timer based spawner
        /*
        elapsed += Time.deltaTime;
        float currentInterval = Mathf.Max(intervalMin, spawnInterval - (elapsed / rampUpTime));
        timer += Time.deltaTime;
        if (timer >= currentInterval)
        {
            timer = 0f;
            SpawnOne();
        }
        */
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
