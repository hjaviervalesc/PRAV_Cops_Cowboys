using DG.Tweening;
using UnityEngine;
using System.Collections;

public class PowerUpManager : SpawnOnTerrainBase
{
    [SerializeField] private Speedable speedablePrefab;
    [SerializeField] private float minX = -20f;
    [SerializeField] private float maxX = 20f;
    [SerializeField] private float minZ = -20f;
    [SerializeField] private float maxZ = 20f;

    private void Start()
    {
        PoolManager.instance.Load(speedablePrefab.gameObject, 3);
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float wait = Random.Range(25f, 35f);
            yield return new WaitForSeconds(wait);

            Vector3 pos = GetPointOnTerrain();

            GameObject obj = PoolManager.instance.Spawn(speedablePrefab.gameObject);

            if (obj != null)
            {
                obj.transform.position = pos;

                Speedable sp = obj.GetComponent<Speedable>();

                sp.OnPickedExtra = player =>
                {
                    var renderer = player.GetComponent<Renderer>();

                    if (WeatherSystem.instance.currentWeather == WeatherType.Hot)
                    {
                        renderer.material.DOColor(new Color(1f, 0.5f, 0f), 0.2f)
                            .OnComplete(() => renderer.material.DOColor(Color.white, 3f));
                    }
                    else
                    {
                        renderer.material.DOColor(new Color(0.3f, 0.6f, 1f), 0.2f)
                            .OnComplete(() => renderer.material.DOColor(Color.white, 3f));
                    }
                };
            }
        }
    }

    private Vector3 GetPointOnTerrain()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        Vector3 origin = new Vector3(x, 100f, z);

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 200f, groundMask))
        {
            return hit.point + Vector3.up * 0.5f; // un poco por encima
        }

        return new Vector3(x, 1f, z);
    }
}