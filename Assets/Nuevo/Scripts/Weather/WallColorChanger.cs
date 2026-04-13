using UnityEngine;

public class WeatherMaterialChanger : MonoBehaviour
{
    [SerializeField] private Material wallMaterial;

    private Color hotColor = Color.red;
    private Color coldColor = Color.white;

    void Update()
    {
            //Es Singleton 
        if (WeatherSystem.instance.currentWeather == WeatherType.Hot)
        {
            wallMaterial.color = hotColor;
        }
        else
        {
            wallMaterial.color = coldColor;
        }
    }
}