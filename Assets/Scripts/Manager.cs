using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : SpawnOnTerrainBase
{
    public GameObject enemyPrefab, explosivePrefab;
    public float minEnemySpawnTimer = 2, maxEnemySpawnTimer = 3;
    public float minExplosiveSpawnTimer = 4, maxExplosiveSpawnTimer = 8;
    public float minEnemySpawnDistance = 7, maxEnemySpawnDistance = 9;

    protected Transform _playerTransform;
    protected PlayerHealth _playerHealth;
    protected List<GameObject> _enemies;

    private void Awake()
    {
        _playerTransform = Player.instance.GetComponent<Transform>();
        _playerHealth = Player.instance.GetComponent<PlayerHealth>();
        _enemies = new List<GameObject>();
    }

    private void Start()
    {
        PoolManager.instance.Load(enemyPrefab, 10);
        PoolManager.instance.Load(explosivePrefab, 1);

        StartCoroutine(SpawnEnemy(enemyPrefab, minEnemySpawnTimer, maxEnemySpawnTimer, minEnemySpawnDistance, maxEnemySpawnDistance));
        StartCoroutine(SpawnExplosion(explosivePrefab, minExplosiveSpawnTimer, maxExplosiveSpawnTimer));
    }

    private IEnumerator SpawnEnemy(GameObject prefab, float minTimeSpawn, float maxTimeSpawn, float minDistance, float maxDistance)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));

            if (_playerHealth.isAlive && _playerTransform != null)
            {
                // 1. Offset alrededor del jugador
                var randomOffset = Random.insideUnitSphere;
                var offset = new Vector3(randomOffset.x, 0, randomOffset.z).normalized * Random.Range(minDistance, maxDistance);

                // 2. Posición XZ
                Vector3 xz = _playerTransform.position + offset;

                // 3. Ajustar al terreno
                Vector3 spawnPoint = GetPointAboveTerrain(xz);

                // 4. Spawn desde el pool
                var go = PoolManager.instance.Spawn(prefab);
                var goTransform = go.transform;

                goTransform.position = spawnPoint;
                goTransform.LookAt(_playerTransform);

                _enemies.Add(go);
            }
        }
    }

    private IEnumerator SpawnExplosion(GameObject prefab, float minTimeSpawn, float maxTimeSpawn)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));

            if (_playerHealth.isAlive && _playerTransform != null)
            {
                // Explosión justo debajo del jugador, pero ajustada al terreno
                Vector3 xz = new Vector3(_playerTransform.position.x, 0, _playerTransform.position.z);
                Vector3 spawnPoint = GetPointAboveTerrain(xz);

                var go = PoolManager.instance.Spawn(prefab);
                go.transform.position = spawnPoint;
            }
        }
    }

    private void Update()
    {
        if ((!_playerHealth.isAlive || _playerTransform == null) && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }
}