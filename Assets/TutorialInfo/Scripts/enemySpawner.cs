using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public struct SpawnCard
{
    public GameObject enemy;
    public int weight;
    public float difficulty;
}
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
    public SpawnCard[] enemyOptions;
    public Transform player;
    public float minRadius = 20f;
    public float maxRadius = 50f;
    public float spawnInterval = 5f;
    public float rampUpTime = 60f; // Time in seconds to reach max spawn rate
    public float intervalMin = 0.5f;

    public int waveCounter = 0;
    public int enemiesToSpawn = 0;
    public bool isWaveClear = true;
    public bool startWave = true;
    public bool isSecondStage = false;
    public int enemiesAlive = 0;

    public GameObject itemSelect;
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
        // secondStageSpawnTimer += Time.deltaTime;

        if (player == null) return;

        // wave based spawner

        if (isWaveClear)
        {
            isWaveClear = false;
            if (startWave)
            {
                waveCounter++;
                List<SpawnCard> deck = new List<SpawnCard>();
                foreach(SpawnCard enemy in enemyOptions)
                {
                    for (int i = 0; i < enemy.weight; i++)
                    {
                        deck.Add(enemy);
                    }
                }

               
                float difficulty = Mathf.CeilToInt(10f * Mathf.Pow(1.2f, (float)waveCounter)); //calculates number of enemies to spawn this wave, exponential growth
              
                startWave = false;
                
                while(difficulty > 0)
                {
                    SpawnCard newEnemy = deck[Random.Range(0, deck.Count)];
                    SpawnOne(newEnemy.enemy);
                    difficulty -= newEnemy.difficulty;
                    enemiesAlive++;
                }

            }



            // 1st stage of wave

            // if (enemiesToSpawn > 0)
            // {
            //     isSecondStage = true;
            //     secondStageSpawnTimer = 0f;
            // }

            // 2nd stage of wave

            // if (enemiesToSpawn == 0)
            // {

            //     //startWave = true;
            // }
        }
        if (enemiesAlive <= 0 && !isWaveClear)
        {
            isWaveClear = true;
            if (!itemSelect.activeSelf)
            {
                itemSelect.SetActive(true);
            }
        }

        // if (isSecondStage && enemiesToSpawn >= 0)
        // {
        //     Debug.LogError(enemiesToSpawn);

        //         float timeBetweenSpawn = Random.Range(3f, 11f); // min is inclusive, max is exclusive

        //         if (secondStageSpawnTimer > timeBetweenSpawn)
        //     {   
        //             // issue when SecondStage needs to run again. 
        //             // Ex. on the first wave there are 11 total enemies. 5 spawn in the first stage, leaving 6 for the second stage.
        //             // The second stage spawns less than 6 enemies and require another iteration of the second stage. 
        //             // However, the second stage will spawn an extra enemy, so 12 total instead of 11.

        //             secondStageSpawnTimer = 0f;
        //             int amountToSpawn = Random.Range(3, 11);
        //             if (amountToSpawn > enemiesToSpawn)
        //             {
        //                 amountToSpawn = enemiesToSpawn;
        //             }

        //             for (int j = 0; j < amountToSpawn; j++) // spawn a group of enemies
        //             {
        //                 SpawnOne();
        //                 enemiesToSpawn--;
        //                 Debug.LogError("2nd stage amountToSpawn j: " + j);
        //             }

        //         }

        //     }

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

    void SpawnOne(GameObject _enemy)
    {
        float r = Random.Range(minRadius, maxRadius);
        float ang = Random.Range(0f, Mathf.PI * 2f);

        Vector3 pos = player.position + new Vector3(Mathf.Cos(ang), 0, Mathf.Sin(ang)) * r;
        var enemy = Instantiate(_enemy, pos, Quaternion.identity);

        enemy.transform.forward = (player.position - pos).normalized;

    }
}
