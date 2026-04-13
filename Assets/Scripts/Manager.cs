using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public GameObject enemyPrefab, explosivePrefab;
    public float minEnemySpawnTimer = 2, maxEnemySpawnTimer = 3, minExplosiveSpawnTimer = 4, maxExplosiveSpawnTimer = 8;        //Explosivo aparece igual que enemigos en tiempo aleatorio (en ejercicio final, que lo hagan pulsando tecla)
    public float minEnemySpawnDistance = 7, maxEnemySpawnDistance = 9;

    protected Transform _playerTransform;
    protected PlayerHealth _playerHealth;
    protected List<GameObject> _enemies;
    protected bool _explosion = true;

    private void Awake()
    {
        _playerTransform = Player.instance.GetComponent<Transform>();
        _playerHealth = Player.instance.GetComponent<PlayerHealth>();
        _enemies = new List<GameObject>();

    }

    // Start is called before the first frame update
    void Start()
    {
        PoolManager.instance.Load(enemyPrefab, 10);
        PoolManager.instance.Load(explosivePrefab, 1);

        StartCoroutine(SpawnEnemy(enemyPrefab, minEnemySpawnTimer, maxEnemySpawnTimer, minEnemySpawnDistance, maxEnemySpawnDistance));
        StartCoroutine(SpawnExplosion(explosivePrefab, minExplosiveSpawnTimer, maxExplosiveSpawnTimer));
    }

    private IEnumerator SpawnEnemy(GameObject prefab, float minTimeSpawn, float maxTimeSpawn, float minDistance, float maxDistance)
    {

        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));

            if(_playerHealth.isAlive && _playerTransform != null)       //Comprobamos que el enemigo esta vivo y sigue en escena
            {
                var randomOffset = Random.insideUnitSphere;     //Esfera alrededor del player
                var offset = new Vector3(randomOffset.x, 0, randomOffset.z).normalized * Random.Range(minDistance, maxDistance);
                var go = PoolManager.instance.Spawn(prefab);
                var goTransform = go.transform;
                goTransform.position = _playerTransform.position + offset;
                goTransform.LookAt(_playerTransform);
                _enemies.Add(go);
            }
        }

       /* while(_enemies.Count <= 10) 
        {
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
            var randomOffset = Random.insideUnitSphere;     //Esfera alrededor del player
            var offset = new Vector3(randomOffset.x, 0, randomOffset.z).normalized * Random.Range(minDistance,maxDistance);
            var go = Instantiate(enemyPrefab);      //Instanciar enemigo
            var goTransform = go.transform;
            goTransform.position = _playerTransform.position + offset;
            goTransform.LookAt(_playerTransform);
            go.GetComponent<Enemy>().target = _playerTransform;     //Asigna al target de Enemy quien es el player sin usar Find
            _enemies.Add(go);
        }*/
    }

    private IEnumerator SpawnExplosion(GameObject prefab, float minTimeSpawn, float maxTimeSpawn)       //Instantiate explosions
    {
        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
        if(_playerHealth.isAlive && _playerTransform != null)
        {
            var go = PoolManager.instance.Spawn(prefab);
            var goTransform = go.transform;
            goTransform.position = _playerTransform.position - new Vector3(0, _playerTransform.position.y,0);       //Para que aparezcan siempre en el suelo
        }
    }

    private void Update()
    {
        if ((!_playerHealth.isAlive || _playerTransform == null) && Input.GetKeyDown(KeyCode.R))            //Reinicia juego al pulsar la R
            SceneManager.LoadScene(0);

        /*if(Input.GetKeyDown(KeyCode.E) && _explosion == true)
        {
            StartCoroutine(SpawnExplosion(explosivePrefab));
            _explosion = false;
        }*/
    }
}
