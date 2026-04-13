using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minEnemySpawnTimer = 2, maxEnemySpawnTimer = 3;
    public float minEnemySpawnDistance = 7, maxEnemySpawnDistance = 9;

    [SerializeField] private Transform _playerTransform;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //private IEnumerator Spawn
}
