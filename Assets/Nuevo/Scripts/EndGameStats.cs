using UnityEngine;

public class EndGameStats : MonoBehaviour
{
    [SerializeField] private JSONLoggerSpeedables speedableLogger;

    public void ShowStats()
    {
        int total = speedableLogger.TotalPickups();
        string lastPickup = speedableLogger.LastPickupTime();

        Debug.Log("Speedables totales: " + total);
        Debug.Log("Último recogido: " + lastPickup);
    }
}