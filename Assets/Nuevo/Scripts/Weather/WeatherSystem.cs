using UnityEngine;
using System.Collections;

public enum WeatherType
{
    Hot,
    Cold
}

public class WeatherSystem : Singleton<WeatherSystem>
{
    public WeatherType currentWeather = WeatherType.Hot;
    [SerializeField] private float changeInterval = 30f;

    private void Start()
    {
        StartCoroutine(AutoChangeWeather());
    }

    public void SetWeather(WeatherType newWeather)
    {
        currentWeather = newWeather;
    }

    private IEnumerator AutoChangeWeather()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeInterval);

            // Alternar clima
            if (currentWeather == WeatherType.Hot)
            {
                currentWeather = WeatherType.Cold;
            }
            else
            {
                currentWeather = WeatherType.Hot;
            }
        }
    }
}