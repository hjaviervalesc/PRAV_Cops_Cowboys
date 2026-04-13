using System.Collections;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject snowBallPrefab;
    [SerializeField] private GameObject sandBallPrefab;

    //Plano donde caeran las bolas
    [SerializeField] private GameObject plane;

    [SerializeField] private float minSpawnTime = 0.5f;
    [SerializeField] private float maxSpawnTime = 1.5f;

    [SerializeField] private WeatherSystem weather;

    private System.Func<GameObject> getPrefab;
    private System.Func<Vector3> getSpawnPoint;
    private System.Func<float> getSpawnTime;

    private void Start()
    {
        // Pool con límite de 7
        PoolManager.instance.Load(snowBallPrefab, 7);
        PoolManager.instance.Load(sandBallPrefab, 7);

        // Lambdas
        getPrefab = () =>
            weather.currentWeather == WeatherType.Cold
            ? snowBallPrefab
            : sandBallPrefab;

        // Punto aleatorio dentro del plano
        getSpawnPoint = () =>
        {
            var renderer = plane.GetComponent<MeshRenderer>();
            Vector3 size = renderer.bounds.size;
            Vector3 center = renderer.bounds.center;

            float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
            float z = Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);

            // Caen desde arriba (10 unidades)
            return new Vector3(x, center.y + 10f, z);
        };

        getSpawnTime = () =>
            Random.Range(minSpawnTime, maxSpawnTime);

        StartCoroutine(SpawnBalls());
    }
    private IEnumerator SpawnBalls()
    {
        while (true)
        {
            yield return new WaitForSeconds(getSpawnTime());

            // Número aleatorio de bolas: de 1 a 4
            int count = Random.Range(1, 5);

            for (int i = 0; i < count; i++)
            {
                GameObject prefab = getPrefab();
                Vector3 point = getSpawnPoint();

                GameObject ball = PoolManager.instance.Spawn(prefab);

                if (ball != null)
                    ball.transform.position = point;
            }
        }
    }

}