using DG.Tweening;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private Speedable speedablePrefab;

    private void Start()
    {
        var sp = Instantiate(speedablePrefab);

        sp.OnPickedExtra += player =>
        {
            var renderer = player.GetComponent<Renderer>();

            if (WeatherSystem.instance.currentWeather == WeatherType.Hot)
            {
                renderer.material.DOColor(new Color(1f, 0.5f, 0f), 0.2f)
                    .OnComplete(() => renderer.material.DOColor(Color.white, 0.2f));
            }
            else
            {
                renderer.material.DOColor(new Color(0.3f, 0.6f, 1f), 0.2f)
                    .OnComplete(() => renderer.material.DOColor(Color.white, 0.2f));
            }
        };
    }
}