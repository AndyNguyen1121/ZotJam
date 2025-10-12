using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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
    public float minimumDistanceFromPlayer = 5f;

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

    public Collider overworldCollider;
    public Collider hellCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            var p = PlayerManager.instance.gameObject;
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

        Vector3 pos = GetColliderPosition();
        var enemy = Instantiate(_enemy, pos, Quaternion.identity);

        enemy.transform.forward = (player.position - pos).normalized;

    }

    public Vector3 GetColliderPosition()
    {
        if (PlayerManager.instance == null)
        {
            Debug.Log("No PlayerManager");
            return Vector3.zero;
        }

        Transform areaTransform = null;
        Vector3 cubeCenter = Vector3.zero;
        Vector3 cubeSize = Vector3.zero;
        Collider currentCollider = null;
        if (PlayerManager.instance.currentLocation == PlayerLocation.Overworld)
        {
            areaTransform = overworldCollider.transform;
            cubeCenter = overworldCollider.bounds.center;
            cubeSize = overworldCollider.bounds.size;
            currentCollider = overworldCollider;
        }
        else
        {
            areaTransform = hellCollider.transform;
            cubeCenter = hellCollider.bounds.center;
            cubeSize = hellCollider.bounds.size;
            currentCollider = hellCollider;
        }

        Vector3 randomPos = cubeCenter + new Vector3(
                Random.Range(-cubeSize.x / 2f, cubeSize.x / 2f),
                0,
                Random.Range(-cubeSize.z / 2f, cubeSize.z / 2f));

        // Adjust position if too close to player
        if (Vector3.Distance(PlayerManager.instance.transform.position, randomPos) < minimumDistanceFromPlayer)
        {
            Vector3 dirToAdjustSpawnLocation = (randomPos - PlayerManager.instance.transform.position).normalized;
            dirToAdjustSpawnLocation.y = 0;
            randomPos = PlayerManager.instance.transform.position + (dirToAdjustSpawnLocation * minimumDistanceFromPlayer);
            randomPos.y = currentCollider.transform.position.y;
        }

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, 4f, NavMesh.AllAreas))
        {
            randomPos = hit.position;
        }
        return randomPos;
    }
}
